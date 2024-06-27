using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class DeliveryMan
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public virtual ApplicationUser User {  get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List <DeliveryManRegions> DeliveryManRegions { get; set;}
        public virtual Branch Branch { get; set; }

    }
}
