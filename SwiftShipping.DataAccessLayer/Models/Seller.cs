using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class Seller
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public string StoreName { get; set; }

        [ForeignKey("Region")]
        public int RegionId { get; set; }

        [ForeignKey("User")]
        public string userId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Region Region { get; set; }
    }
}
