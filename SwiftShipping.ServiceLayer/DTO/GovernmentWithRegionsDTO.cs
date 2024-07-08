using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class GovernmentWithRegionsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RegionGetDTO> Regions { get; set; }
    }
}
