using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("User")]
        public string userId { get; set; }

        public bool IsDeleted { get; set; } = false;


        public virtual ApplicationUser User { get; set; }
    }
}
