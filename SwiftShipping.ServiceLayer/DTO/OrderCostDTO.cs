using SwiftShipping.DataAccessLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class OrderCostDTO
    {
        public float Weight{ get; set; }
        public OrderType OrderType{ get; set; }
        public ShippingType ShippingType { get; set; }
        public bool IsShippedToVillage { get; set; } = false;
        public int RegionId { get; set; }
    }
}
