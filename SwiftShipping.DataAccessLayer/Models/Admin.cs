using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Models
{
    internal class Admin
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string userId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
