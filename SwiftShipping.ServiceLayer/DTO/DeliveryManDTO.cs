using SwiftShipping.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class DeliveryManDTO
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public int branchId { get; set; }

    }
}
