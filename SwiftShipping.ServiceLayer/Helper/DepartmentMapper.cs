using SwiftShipping.DataAccessLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Helper
{
    public static class DepartmentMapper
    {
        public static Dictionary<Department, string> DepartmentsDictionary = new Dictionary<Department, string>()
        {
            { Department.Employees, "Employees" },
            { Department.DeliveryMen, "Delivery Men" },
            { Department.Sellers, "Sellers" },
            { Department.Admins, "Admins" },
            { Department.Branches, "Branches" },
            { Department.Governments, "Governments" },
            { Department.Regions, "Regions" },
            { Department.Orders, "Orders" },
            { Department.WeightSetting, "Weight Setting" }


        };
    }
}
