using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal NormalPrice { get; set; }
        public decimal PickupPrice { get; set; }
        [ForeignKey("Government")]
        public int GovernmentId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual Government Government { get; set; }
        public virtual List<DeliveryManRegions> DeliveryManRegions {get;set;}
    }
}
