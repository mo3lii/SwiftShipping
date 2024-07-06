using Microsoft.AspNetCore.Authorization;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Permissions;
using SwiftShipping.ServiceLayer.Helper;

namespace SwiftShipping.API.Authorization
{
    public static class AuthorizationPolicies
    {
        public static AuthorizationOptions AddDepartmentsPolicies(AuthorizationOptions options)
        {

            var departments = Department.GetValues(typeof(Department)).Cast<Department>().ToList();
            var permissions = PermissionType.GetNames(typeof(RoleTypes)).ToList();

            foreach (var department in departments)
            {
                foreach (var permission in permissions)
                {
                    //policy name : DepartmentName/Permission 
                    options.AddPolicy($"{DepartmentMapper.DepartmentsDictionary[department]}/{permission}", policy =>
                     policy.RequireAuthenticatedUser()
                   .AddRequirements(new PermissionRequirement(Department.Employees, PermissionType.View)));
                }
            }
            return options;

        }
    }
}
