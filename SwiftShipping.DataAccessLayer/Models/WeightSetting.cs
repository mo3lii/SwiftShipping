using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class WeightSetting
    {
        [Key]
        public int Id { get; set; }
        public float DefaultWeight { get; set; } //"defaultWeight"
        public decimal KGPrice { get; set; }
    }
}
