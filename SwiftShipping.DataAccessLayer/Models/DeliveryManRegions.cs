using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class DeliveryManRegions
    {
        
        [ForeignKey("DeliveryMan")]
        public int DeliveryManId { get; set; }
        [ForeignKey("Region")]
        public int RegionId { get; set; }

        public virtual DeliveryMan DeliveryMan { get; set; }
        public virtual Region Region { get; set; }
        public virtual Branch Branch { get; set; }

    }
}
