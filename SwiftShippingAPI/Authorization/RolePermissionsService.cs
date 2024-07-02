using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Repository;

namespace SwiftShipping.API.Authorization
{
    public class RolePermissionsService : IRolePermissionsService
    {
        private readonly UnitOfWork _unit;

        public RolePermissionsService(UnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<bool> CheckPermissionAsync(string role, Department department, PermissionType permissionType)
        {
            var rolePermissions = _unit.RolePermissionsRepository.GetById(role, department);

            if (rolePermissions == null)
            {
                return false;
            }

            return permissionType switch
            {
                PermissionType.Add => rolePermissions.Add,
                PermissionType.View => rolePermissions.View,
                PermissionType.Edit => rolePermissions.Edit,
                PermissionType.Delete => rolePermissions.Delete,
                _ => false,
            };
        }
    }
}
