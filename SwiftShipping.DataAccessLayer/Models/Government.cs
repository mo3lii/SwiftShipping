using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class Government
    {
        public int Id {  get; set; }
        public string Name { get; set; }    
        public virtual List<Region> Regions {  get; set; }
    }
}
