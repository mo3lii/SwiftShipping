using AutoMapper;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
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
    }
}
