using SwiftShipping.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class OrderGetDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string Government { get; set; }
        public bool IsShippedToVillage { get; set; }
        public string? VillageName { get; set; }
        public float Weight { get; set; }
        public string BranchName { get; set; }
        public decimal OrderPrice { get; set; }
        public string? Note { get; set; }
        public string ShippingType { get; set; }
        public string OrderType { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set;}
    }
}
