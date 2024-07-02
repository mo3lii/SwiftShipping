using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SwiftShipping.API.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IRolePermissionsService _rolePermissionsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionHandler(IRolePermissionsService rolePermissionsService, IHttpContextAccessor httpContextAccessor)
        {
            _rolePermissionsService = rolePermissionsService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            var currentRole = currentUser.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(currentRole))
            {
                var isAuthorized = await _rolePermissionsService.CheckPermissionAsync(currentRole, requirement.Department, requirement.PermissionType);

                if (isAuthorized)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            context.Fail(); // Fail the requirement if the user's role does not match or is not found
        }
    }
}
