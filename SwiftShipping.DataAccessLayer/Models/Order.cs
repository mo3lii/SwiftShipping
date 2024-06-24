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
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Address { get; set; }
        [ForeignKey("Government")]
        public int GovernmentId { get; set; }
        [ForeignKey("Region")]
        public int RegionId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool isShippedToVillage {  get; set; }
        public string? VillageName { get; set;}
        public float Weight { get; set; }
        public decimal Cost { get; set; }
        [ForeignKey("DeliveryMan")]
        public string DeliveryId { get; set; }
        [ForeignKey("OrderStatus")]
        public int StatusId { get; set; }
        public string Note { get;set; }

        public ShippingType ShippingType { get; set; }
        public ShippingTime ShippingTime { get; set; }

        public virtual ApplicationUser DeliveryMan {  get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual Government Government { get; set; }
        public virtual Region Region { get; set; }
    }
}
