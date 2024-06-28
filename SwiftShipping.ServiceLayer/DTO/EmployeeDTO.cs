using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class EmployeeDTO
    {
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }

        public string userName { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public int BranchId { get; set; }
    }
}
