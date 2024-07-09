using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class AssignRegionsDTO
    {
        public int DeliveryManId { get; set; }
        public int[] RegionIds { get; set; }
    }
}
