using Microsoft.AspNetCore.Identity;
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
    public class EmployeeService
    {
        private UnitOfWork unit;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public EmployeeService(UnitOfWork _unit, UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager, SignInManager<ApplicationUser> _signInManager)
        {
            unit = _unit;
            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
        }

        public async Task<bool> addEmployeeAsync(EmployeeDTO employeeDTO)
        {
            ApplicationUser appUser = new ApplicationUser()
            {
                Name = employeeDTO.name,
                Email = employeeDTO.email,
                Address = employeeDTO.address,
                UserName = employeeDTO.userName,
                PasswordHash = employeeDTO.password,
                PhoneNumber = employeeDTO.phoneNumber,
            };

            IdentityResult result = await userManager.CreateAsync(appUser, employeeDTO.password);
            if (result.Succeeded)
            {

                // check if the role is exist if not, add it
                if (await roleManager.FindByNameAsync("employee") == null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "employee" });

                // assign roles  to created user
                IdentityResult employeeRole = await userManager.AddToRoleAsync(appUser, "employee");


                if (employeeRole.Succeeded)
                {

                    Employee employee = new Employee()
                    {
                        UserId = appUser.Id,
                        Name = employeeDTO.name,
                        RegionId = employeeDTO.regionId,
                    };
                    unit.EmployeeRipository.Insert(employee);
                    unit.SaveChanges();
                    return true;
                }

            }

            return false;

        }

        
        public async Task<(bool Success, string UserId,string Role)> Login(LoginDTO loginDTO)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(loginDTO.email);

            if (user == null)
            {
                user = await userManager.FindByNameAsync(loginDTO.email);
            }

            if (user != null)
            {
                bool found = await userManager.CheckPasswordAsync(user, loginDTO.password);
                if (found)
                {
                    await signInManager.SignInAsync(user, loginDTO.RemembreMe);
                    // Fetch user roles
                    var roles = await userManager.GetRolesAsync(user);
                    string role = roles.FirstOrDefault(); 

                    return (true, user.Id,role);
                }
            }

            return (false, null,null);
        }

    }
}
