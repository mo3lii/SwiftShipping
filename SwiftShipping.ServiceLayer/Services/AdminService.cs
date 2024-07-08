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
    public class AdminService
    {
        private readonly UnitOfWork _unit;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminService(UnitOfWork unit, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _unit = unit;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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

        public async Task<bool> addAdminAsync()
        {

            var appUser = new ApplicationUser()
            {
                Name = "admin",
                UserName = "admin",
                Email = "admin@gmail.com",
                PhoneNumber = "0235188423",
                Address = "admin address"
                ,PasswordHash= "Admin@123"
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, "Admin@123");
            if (result.Succeeded)
            {
                //Employee Role as String
                var AdminRole = RoleTypes.Admin.ToString();

                // check if the role is exist if not, add it
                if (await _roleManager.FindByNameAsync(AdminRole) == null)
                    await _roleManager.CreateAsync(new IdentityRole() { Name = AdminRole });

                // assign roles  to created user
                IdentityResult adminRoleAssigning = await _userManager.AddToRoleAsync(appUser, AdminRole);

                if (adminRoleAssigning.Succeeded)
                {
                    var admin = new Admin()
                    {
                        Name = "Admin",
                        IsDeleted = false,
                        userId = appUser.Id,
                    };
                    _unit.AdminRipository.Insert(admin);
                    _unit.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
