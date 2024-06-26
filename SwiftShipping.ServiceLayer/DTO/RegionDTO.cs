using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class RegionDTO
    {
        public string name {  get; set; }
        public decimal normalPrice { get; set; }
        public decimal pickupPrice { get; set; }
        public int governmentId { get; set; }

    }
}
