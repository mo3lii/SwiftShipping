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
        private UnitOfWork _unit;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountService(UnitOfWork unit, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _unit = unit;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public async Task<(bool Success, string UserId, string Role)> LoginWithEmail(LoginWithEmailDTO loginDTO)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(loginDTO.email);

            if (user != null)
            {
                bool found = await _userManager.CheckPasswordAsync(user, loginDTO.password);

                if (found)
                {
                    await _signInManager.SignInAsync(user, loginDTO.RemembreMe);
                    // Fetch user roles
                    var roles = await _userManager.GetRolesAsync(user);
                    string role = roles.FirstOrDefault();

                    return (true, user.Id, role);
                }
            }

            return (false, null, null);
        }

        public async Task<(bool Success, string UserId, string Role)> Login(LoginWithUserNameDTO loginDTO)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(loginDTO.userName);

            if (user != null)
            {
                bool found = await _userManager.CheckPasswordAsync(user, loginDTO.password);

                if (found)
                {
                    await _signInManager.SignInAsync(user, loginDTO.RemembreMe);
                    // Fetch user roles
                    var roles = await _userManager.GetRolesAsync(user);
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
                    return _unit.EmployeeRipository.GetFirstByFilter(e => e.UserId == userId).Id;

                case RoleTypes.Seller:
                    return _unit.SellerRipository.GetFirstByFilter(e => e.UserId == userId).Id;

                case RoleTypes.DeliveryMan:
                    return _unit.DeliveryManRipository.GetFirstByFilter(e => e.UserId == userId).Id;

                case RoleTypes.Admin:
                    return _unit.AdminRipository.GetFirstByFilter(e => e.userId == userId).Id;

                default:
                    throw new InvalidOperationException("Unknown role type");
            }
        }

    }
}
