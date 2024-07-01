using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwiftShipping.DataAccessLayer.Enum;

namespace SwiftShipping.DataAccessLayer.Models
{
    public class RolePermissions
    {
        public string RoleName { get; set; }
        public Department DepartmentId { get; set; }
        public bool View {  get; set; }=false;
        public bool Edit { get; set; } =false;
        public bool Delete { get; set; } = false;
        public bool Add { get; set; } = false;

    }
}
