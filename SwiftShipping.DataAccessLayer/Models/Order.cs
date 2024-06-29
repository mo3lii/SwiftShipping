using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Address { get; set; }
        [ForeignKey("Region")]
        public int RegionId { get; set; }
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool isShippedToVillage {  get; set; }
        public string? VillageName { get; set;}
        public float Weight { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal DeliveryCost { get; set; }
        
        [ForeignKey("DeliveryMan")]
        public int? DeliveryId { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentType PaymentType { get; set; }
        public string? Note { get;set; }
        public ShippingType ShippingType { get; set; }
        public OrderType OrderType { get; set; }
        public bool IsDeleted { get; set; }=false;
        ///objects
        public virtual DeliveryMan DeliveryMan {  get; set; }
        public virtual Region Region { get; set; }
        public virtual Branch Branch { get; set; }  
        public virtual Seller Seller { get; set; }
        
    }
}
