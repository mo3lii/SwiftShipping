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
        public OrderService(UnitOfWork _unit)
        {
            unit = _unit;
        }
        public decimal CalculateOrderCost(float Weight, Region region, ShippingType shippingType)
        {
            var settings = unit.WeightSettingRepository.GetSetting();
            float MaxFreeWeight = settings.DefaultWeight;
            decimal extraKiloPrice = settings.KGPrice;
            decimal calculatedPrice = 0;

            if (shippingType == ShippingType.PickUp)
            {
                calculatedPrice = region.PickupPrice;
            }
            else if (shippingType == ShippingType.Normal)
            {
                calculatedPrice = region.NormalPrice;
            }

            if (Weight > MaxFreeWeight)
            {
                float extraWeight = Weight - MaxFreeWeight;
                calculatedPrice += (decimal)extraWeight * extraKiloPrice;
            }
            return calculatedPrice;
        }

        public bool AddOrder(OrderDTO orderDTO)
        {
            try
            {
                var order = new Order()
                {
                    CustomerName = orderDTO.customerName,
                    CustomerPhone = orderDTO.customerPhone,
                    Address = orderDTO.address,
                    GovernmentId = orderDTO.governmentId,
                    RegionId = orderDTO.regionId,
                    isShippedToVillage = orderDTO.isShippedToVillage,
                    Weight = orderDTO.weight,
                    VillageName = orderDTO.villageName,
                    Note = orderDTO.note,
                    CreationDate = DateTime.Now,
                    StatusId = 1,
                    ShippingTime = ShippingTime.SameDay,
                    ShippingType = ShippingType.Normal
                };
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
    }
}
