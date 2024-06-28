using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public  class BranchGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GovernmentName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
