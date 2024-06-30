using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace SwiftShipping.ServiceLayer.TestAuth
{
    public interface IRoleClaimsService
    {
        Task<bool> CheckRoleClaimAsync(string role, string claimType, string claimValue);
    }


    public class RoleClaimsService : IRoleClaimsService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleClaimsService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CheckRoleClaimAsync(string role, string claimType, string claimValue)
        {
            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity == null)
            {
                return false;
            }

            var claims = await _roleManager.GetClaimsAsync(roleEntity);

            return claims.Any(c => c.Type == claimType && c.Value == claimValue);
        }
    }


    public class RoleClaimRequirement : IAuthorizationRequirement
    {
        //public string Role { get; }
        public string ClaimType { get; }
        //public string ClaimValue { get; }

        public RoleClaimRequirement( string claimType)
        {
            //Role = role;
            ClaimType = claimType;
            //ClaimValue = claimValue;
        }
    }




    public class RoleClaimHandler : AuthorizationHandler<RoleClaimRequirement>
    {
        private readonly IRoleClaimsService _roleClaimsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleClaimHandler(IRoleClaimsService roleClaimsService, HttpContextAccessor httpContextAccessor)
        {
            _roleClaimsService = roleClaimsService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleClaimRequirement requirement)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            var currentRole = currentUser.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(currentRole))
            {
                var isAuthorized = await _roleClaimsService.CheckRoleClaimAsync(currentRole, requirement.ClaimType, "true");

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
