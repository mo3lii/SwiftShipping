using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Government")]
        public int GovernmentId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual Government Government { get; set; }
        public virtual List<DeliveryMan> DeliveryMen {  get; set; }
        public virtual List<Order> Orders { get; set; }

        public virtual List<Seller> Sellers { get; set; }
    }
}
