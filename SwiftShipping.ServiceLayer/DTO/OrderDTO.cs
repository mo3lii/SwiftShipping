using SwiftShipping.DataAccessLayer.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class OrderDTO
    {

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(3, ErrorMessage = "lenght must be at leat 3")]
        [MaxLength(50, ErrorMessage = "maximum length is 50")]
        [RegularExpression(@"^[\d]{3,}", ErrorMessage = "name must start with at least 3 charachters")]
        public string customerName { get; set; }

        [RegularExpression(@"^[0](10|11|12|15)[0-9]{8}$", ErrorMessage = "Invalid phone number format")]
        public string customerPhone { get; set; }

        [Required(ErrorMessage = "address is Required")]
        [MinLength(3, ErrorMessage = "lenght must be at leat 3")]
        [MaxLength(50, ErrorMessage = "maximum length is 50")]
        [RegularExpression(@"^[\d]{3,}", ErrorMessage = "address must start with at least 3 charachters")]
        public string address { get; set; }


        [Required(ErrorMessage = "Branch is Required")]
        public int branchId { get; set; }

        [Required(ErrorMessage = "Region is Required")]
        public int regionId { get; set; }

        [Required(ErrorMessage = "isShippedToVillage? is Required")]
        public bool isShippedToVillage { get; set; }
        public string? villageName { get; set; }

        [Required(ErrorMessage = "weight is Required")]
        public float weight { get; set; }

        [Required(ErrorMessage = "order price is Required")]
        [RegularExpression(@"\d+", ErrorMessage ="order price must be number")]
        public decimal orderPrice { get; set; }
        public string? note { get; set; }

        [Required(ErrorMessage = "sellerId is Required")]
        [RegularExpression(@"\d+", ErrorMessage = "order price must be number")]
        public int sellerId { get; set; }

        [Required(ErrorMessage = "shippingType is Required")]
        public ShippingType shippingType { get; set; }

        [Required(ErrorMessage = "ordertype is Required")]
        public OrderType orderType { get; set; }

        [Required(ErrorMessage = "paymentType is Required")]
        public PaymentType paymentType { get; set; }

        //delivery man id
        public int? DeliveryId { get; set; }


    }
}
