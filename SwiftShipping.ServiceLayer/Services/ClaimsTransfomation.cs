using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    public class ClaimsTransformation : IClaimsTransformation
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ClaimsTransformation(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;
            var user = await _userManager.GetUserAsync(principal);
            if (user == null)
            {
                return principal;
            }

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(role));
                identity.AddClaims(roleClaims);
            }

            return principal;
        }
    }
}
