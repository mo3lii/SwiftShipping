using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    public class AccountService
    {
        private UnitOfWork unit;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountService(UnitOfWork _unit, UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager, SignInManager<ApplicationUser> _signInManager, IMapper _mapper)
        {
            unit = _unit;
            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
        }
        public async Task<(bool Success, string UserId, string Role)> LoginWithEmail(LoginWithEmailDTO loginDTO)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(loginDTO.email);

            if (user != null)
            {
                bool found = await userManager.CheckPasswordAsync(user, loginDTO.password);

                if (found)
                {
                    await signInManager.SignInAsync(user, loginDTO.RemembreMe);
                    // Fetch user roles
                    var roles = await userManager.GetRolesAsync(user);
                    string role = roles.FirstOrDefault();

                    return (true, user.Id, role);
                }
            }

            return (false, null, null);
        }

        public async Task<(bool Success, string UserId, string Role)> Login(LoginWithUserNameDTO loginDTO)
        {
            ApplicationUser user = await userManager.FindByNameAsync(loginDTO.userName);

            if (user != null)
            {
                bool found = await userManager.CheckPasswordAsync(user, loginDTO.password);

                if (found)
                {
                    await signInManager.SignInAsync(user, loginDTO.RemembreMe);
                    // Fetch user roles
                    var roles = await userManager.GetRolesAsync(user);
                    string role = roles.FirstOrDefault();

                    return (true, user.Id, role);
                }
            }

            return (false, null, null);
        }

        public async Task<int> getIdByRole(string userId, string role)
        {

            if (!Enum.TryParse(role, true, out RoleTypes roleType))
            {
                throw new InvalidOperationException("Unknown role type");
            }

            switch (roleType)
            {
                case RoleTypes.Employee:
                    return unit.EmployeeRipository.GetFirstByFilter(e => e.UserId == userId).Id;

                case RoleTypes.Seller:
                    return unit.SellerRipository.GetFirstByFilter(e => e.UserId == userId).Id;

                case RoleTypes.DeliveryMan:
                    return unit.DeliveryManRipository.GetFirstByFilter(e => e.UserId == userId).Id;

                case RoleTypes.Admin:
                    return unit.AdminRipository.GetFirstByFilter(e => e.userId == userId).Id;

                default:
                    throw new InvalidOperationException("Unknown role type");
            }
        }

    }
}
