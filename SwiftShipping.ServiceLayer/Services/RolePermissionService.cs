using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    public class RolePermissionService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolePermissionService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task AssignPermissionsToRoleAsync(string roleName, List<string> permissions)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                foreach (var permission in permissions)
                {
                    var claim = new Claim(permission, "true");
                    if (!(await _roleManager.GetClaimsAsync(role)).Any(c => c.Type == permission))
                    {
                        await _roleManager.AddClaimAsync(role, claim);
                    }
                }
            }
        }

        public async Task AssignPermissionsToRoleAsync(string roleName, string permission)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                    var claim = new Claim(permission, "true");
                    if (!(await _roleManager.GetClaimsAsync(role)).Any(c => c.Type == permission))
                    {
                        await _roleManager.AddClaimAsync(role, claim);
                    }
            }
        }
        public async Task RemovePermissionFromRoleAsync(string roleName, string permission)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                var claim = claims.FirstOrDefault(c => c.Type == permission);
                if (claim != null)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }
            }
        }
    }

}
