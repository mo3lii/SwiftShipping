using AutoMapper;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    public class OrderService
    {
        private UnitOfWork unit;
        private readonly IMapper mapper;
        public OrderService(UnitOfWork _unit,IMapper mapper)
        {
            unit = _unit;
            this.mapper = mapper;
        }

        public decimal CalculateOrderCost(OrderCostDTO order )
        {
            var settings = unit.WeightSettingRepository.GetSetting();
            float MaxFreeWeight = settings.DefaultWeight;
            decimal extraKiloPrice = settings.KGPrice;
            decimal calculatedPrice = 0;

            var Region = unit.RegionRipository.GetById(order.RegionId);
            if (order.OrderType == OrderType.PickUp)
            {
                calculatedPrice = Region.PickupPrice;
            }
            else if (order.OrderType == OrderType.Normal)
            {
                calculatedPrice = Region.NormalPrice;
            }

            switch (order.ShippingType)
            {
                case ShippingType.SameDay:
                    calculatedPrice += 50m;
                    break;
                case ShippingType.In24H:
                    calculatedPrice += 30m;
                    break;
                    //case ShippingType.In2to5Days:
                    //    calculatedPrice += 15m;
                    //    break;
            }

            if (order.IsShippedToVillage)
            {
                calculatedPrice += 15m;
            }


            if (order.Weight > MaxFreeWeight)
            {
                float extraWeight = order.Weight - MaxFreeWeight;
                calculatedPrice += (decimal)extraWeight * extraKiloPrice;
            }
            return calculatedPrice;
        }
        public decimal CalculateOrderCost(Order order)
        {
            var settings = unit.WeightSettingRepository.GetSetting();
            float MaxFreeWeight = settings.DefaultWeight;
            decimal extraKiloPrice = settings.KGPrice;
            decimal calculatedPrice = 0;

            if (order.OrderType == OrderType.PickUp)
            {
                calculatedPrice = order.Region.PickupPrice;
            }
            else if (order.OrderType == OrderType.Normal)
            {
                calculatedPrice = order.Region.NormalPrice;
            }

            switch (order.ShippingType)
            {
                case ShippingType.SameDay:
                    calculatedPrice += 50m;
                    break;
                case ShippingType.In24H:
                    calculatedPrice += 30m;
                    break;
                //case ShippingType.In2to5Days:
                //    calculatedPrice += 15m;
                //    break;
            }

            if(order.isShippedToVillage)
            {
                calculatedPrice += 15m;
            }
          

            if (order.Weight > MaxFreeWeight)
            {
                float extraWeight = order.Weight - MaxFreeWeight;
                calculatedPrice += (decimal)extraWeight * extraKiloPrice;
            }
            return calculatedPrice;
        }

        public bool AddOrder(OrderDTO orderDTO)
        {
            try
            {
                var order = mapper.Map<OrderDTO, Order>(orderDTO);
                order.Status=OrderStatus.New;
                order.CreationDate = DateTime.Now;
                //assign region before send order to get the price
                order.Region = unit.RegionRipository.GetById(order.RegionId);
                order.DeliveryCost = CalculateOrderCost(order);
                unit.OrderRipository.Insert(order);
                unit.SaveChanges();
                return true;
            }catch 
            {
                return false;
            }
        }


        public bool AssignOrderToDeliveryMan(int orderID, int deliveryManID)
        {
            Order order = unit.OrderRipository.GetFirstByFilter(o => o.Id == orderID);
            if (order != null)
            {
                // Update the delivery man ID
                order.DeliveryId = deliveryManID;
                // Save changes
                unit.OrderRipository.Update(order);
                unit.SaveChanges();
                return true; 
            }
            return false; 
        }

        public List<OrderGetDTO> GetAll()
        {
            var orders = unit.OrderRipository.GetAll();
            return mapper.Map<List<Order>, List<OrderGetDTO>>(orders);
        }

        public OrderGetDTO GetById(int id)
        {
            var order = unit.OrderRipository.GetById(id);
            return mapper.Map<Order, OrderGetDTO>(order);

        }
   
        public List<OrderGetDTO> GetByStatus(OrderStatus orderStatus)
        {
            var orders = unit.OrderRipository.GetAll(o => o.Status == orderStatus).ToList();
            return mapper.Map<List<Order>, List<OrderGetDTO>>(orders);
        }

        public EnumDTO GetOrderStatusCount(OrderStatus orderStatus)
        {
             int count = unit.OrderRipository.GetAll(o => o.Status == orderStatus).Count;

            return (new EnumDTO() { Name = StatusMapper.StatusDictionary[orderStatus], Count =  count, Id =  (int) orderStatus });
        }

        public List<EnumDTO> GetAllOrderStatusCount()
        {
            // get status, count
            var res = unit.OrderRipository.GetAll().GroupBy(x => x.Status).ToDictionary(g => g.Key, g => g.Count());

            foreach (var status in StatusMapper.StatusDictionary)
            {
                if (res.ContainsKey(status.Key) == false)
                    res.Add(status.Key, 0);
            }

            List<EnumDTO> statusWithCount =
                res.Select(x=> new EnumDTO() { Name = StatusMapper.StatusDictionary[x.Key], 
                    Count = x.Value, Id = (int) x.Key}).ToList();

            return statusWithCount;

        }
        public EnumDTO GetOrderStatusCountForSeller(OrderStatus orderStatus, int sellerId)
        {
            int count = unit.OrderRipository.GetAll(order => order.Status == orderStatus && order.SellerId == sellerId).Count;

            return (new EnumDTO() { Name = StatusMapper.StatusDictionary[orderStatus], Count = count, Id = (int)orderStatus });
        }

        public List<EnumDTO> GetAllOrderStatusCountForSeller(int sellerId)
        {
            // get status, count
            var res = unit.OrderRipository.GetAll(order => order.SellerId == sellerId)
                .GroupBy(x => x.Status)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var status in StatusMapper.StatusDictionary)
            {
                if (res.ContainsKey(status.Key) == false)
                    res.Add(status.Key, 0);
            }

            List<EnumDTO> statusWithCount =
                res.Select(x => new EnumDTO()
                {
                    Name = StatusMapper.StatusDictionary[x.Key],
                    Count = x.Value,
                    Id = (int)x.Key
                }).ToList();

            return statusWithCount;
        }

        public EnumDTO GetOrderStatusCountForDelivary(OrderStatus orderStatus, int delivaryId)
        {
            int count = unit.OrderRipository.GetAll(order => order.Status == orderStatus && order.DeliveryId == delivaryId).Count;

            return (new EnumDTO() { Name = StatusMapper.StatusDictionary[orderStatus], Count = count, Id = (int)orderStatus });
        }

        public List<EnumDTO> GetAllOrderStatusCountForDelivary(int delivaryId)
        {
            // get status, count
            var res = unit.OrderRipository.GetAll(order => order.DeliveryId == delivaryId)
                .GroupBy(x => x.Status)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var status in StatusMapper.StatusDictionary)
            {
                if (res.ContainsKey(status.Key) == false)
                    res.Add(status.Key, 0);
            }

            List<EnumDTO> statusWithCount =
                res.Select(x => new EnumDTO()
                {
                    Name = StatusMapper.StatusDictionary[x.Key],
                    Count = x.Value,
                    Id = (int)x.Key
                }).ToList();

            return statusWithCount;
        }

        public List<ShippingTypeDto> GetShippingTypes()
        {
            var shippingTypesDto = Enum.GetValues(typeof(ShippingType)).Cast<ShippingType>()
                .Select(x => new ShippingTypeDto() 
                { 
                    Id = (int)x, 
                    Name = ShippingTypeMapper.ShippingTypeDictionary[x],
                })
                .ToList();
            return shippingTypesDto;
        }
        public List<OrderTypeDTO> GetOrderTypes()
        {
            var orderTypes = 
                Enum.GetValues(typeof(OrderType)).Cast<OrderType>().Select(
                    x => new OrderTypeDTO() { 
                        Id = (int)x,
                        Name = OrderTypeMapper.OrderTypeDictionary[x] }).ToList();
            return orderTypes;
        }

        public List<PaymentTypeDTO> GetPaymentTypes()
        {
            var orderTypes =
                Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().Select(
                    x => new PaymentTypeDTO()
                    {
                        Id = (int)x,
                        Name = PaymentTypeMapper.PaymentTypeDictionary[x]
                    }).ToList();
            return orderTypes;
        }

        public bool ChangeOrderStatus(OrderStatus status, int orderId)
        {
            try
            {
                var order = unit.OrderRipository.GetById(orderId);

                if (order != null)
                {
                    order.Status = status;
                    unit.OrderRipository.Update(order);
                    unit.SaveChanges();
                    return true;
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }

            return false; 
        }

        public bool UpdateOrder(int id, OrderDTO orderDTO)
        {
            try
            {
                var foundOrder = unit.OrderRipository.GetById(id);
                //app user
                if (foundOrder == null)
                {
                    return false;
                }

                mapper.Map(orderDTO, foundOrder);
                unit.OrderRipository.Update(foundOrder);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteOrder(int id)
        {
            try
            {
                var foundOrder = unit.OrderRipository.GetById(id);
                if (foundOrder == null)
                {
                    return false;
                }
                foundOrder.IsDeleted = true;
                unit.OrderRipository.Update(foundOrder);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    
    
    }
}
