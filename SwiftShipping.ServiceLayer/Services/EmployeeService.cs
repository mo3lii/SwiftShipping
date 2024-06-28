using AutoMapper;
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
        private readonly IMapper mapper; 

        public EmployeeService(UnitOfWork _unit, UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager, SignInManager<ApplicationUser> _signInManager,IMapper _mapper)
        {
            unit = _unit;
            mapper = _mapper;
            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
        }

        public async Task<bool> addEmployeeAsync(EmployeeDTO employeeDTO)
        {
          
            var appUser = mapper.Map<EmployeeDTO, ApplicationUser>(employeeDTO);
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
                    var employee = mapper.Map<EmployeeDTO, Employee>(employeeDTO);
                    employee.UserId = appUser.Id;
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
