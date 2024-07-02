using SwiftShipping.DataAccessLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class PermissionDTO
    {
        public Department DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool View { get; set; } = false;
        public bool Edit { get; set; } = false;
        public bool Delete { get; set; } = false;
        public bool Add { get; set; } = false;
    }
}
