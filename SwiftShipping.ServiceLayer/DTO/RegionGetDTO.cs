using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class RegionGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal NormalPrice { get; set; }
        public decimal PickupPrice { get; set; }
        public int GovernmentId { get; set; }
        public string GovernmentName { get; set; }
    }
}
