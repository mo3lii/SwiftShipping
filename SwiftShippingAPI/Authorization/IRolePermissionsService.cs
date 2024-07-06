using SwiftShipping.DataAccessLayer.Enum;

namespace SwiftShipping.API.Authorization
{
    public interface IRolePermissionsService
    {
        Task<bool> CheckPermissionAsync(string role, Department department, PermissionType permissionType);
    }
}
