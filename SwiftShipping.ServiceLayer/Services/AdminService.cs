﻿using AutoMapper;
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
        private UnitOfWork unit;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminService(UnitOfWork _unit, UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager, SignInManager<ApplicationUser> _signInManager, IMapper _mapper)
        {
            unit = _unit;
            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
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

            IdentityResult result = await userManager.CreateAsync(appUser, "Admin@123");
            if (result.Succeeded)
            {
                //Employee Role as String
                var AdminRole = RoleTypes.Admin.ToString();

                // check if the role is exist if not, add it
                if (await roleManager.FindByNameAsync(AdminRole) == null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = AdminRole });

                // assign roles  to created user
                IdentityResult adminRoleAssigning = await userManager.AddToRoleAsync(appUser, AdminRole);

                if (adminRoleAssigning.Succeeded)
                {
                    var admin = new Admin()
                    {
                        Name = "Admin",
                        IsDeleted = false,
                        userId = appUser.Id,
                    };
                    unit.AdminRipository.Insert(admin);
                    unit.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
