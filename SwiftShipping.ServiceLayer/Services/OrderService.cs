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
                    Note = orderDTO.note
                };
                unit.OrderRipository.Insert(order);
                return true;
            }catch 
            {
                return false;
            }
        }
    }
}
