using Microsoft.AspNetCore.Authorization;
using SwiftShipping.DataAccessLayer.Enum;

namespace SwiftShipping.API.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public Department Department { get; }
        public PermissionType PermissionType { get; }

        public PermissionRequirement(Department department, PermissionType permissionType)
        {
            Department = department;
            PermissionType = permissionType;
        }
    }
}
