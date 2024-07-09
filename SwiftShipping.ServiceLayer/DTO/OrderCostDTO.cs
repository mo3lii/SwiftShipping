using SwiftShipping.DataAccessLayer.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class OrderCostDTO
    {
        [Required(ErrorMessage = "weight field is Required")]
        [RegularExpression(@"[\d]+", ErrorMessage = "weight must be a number")]

        public float Weight{ get; set; }

        [Required(ErrorMessage = "ordertype field is Required")]
        public OrderType OrderType{ get; set; }

        [Required(ErrorMessage = "sgipping type field is Required")]
        public ShippingType ShippingType { get; set; }
        public bool IsShippedToVillage { get; set; } = false;

        [Required(ErrorMessage = "region field is Required")]
        public int RegionId { get; set; }
    }
}
