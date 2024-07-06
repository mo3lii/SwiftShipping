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
                //Employee Role as String
                var EmployeeRole = RoleTypes.Employee.ToString();

                // check if the role is exist if not, add it
                if (await roleManager.FindByNameAsync(EmployeeRole) == null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = EmployeeRole });

                // assign roles  to created user
                IdentityResult employeeRole = await userManager.AddToRoleAsync(appUser, EmployeeRole);


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
                user = await userManager.FindByNameAsync(loginDTO.userName);
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


        public EmployeeDTO GetById(int id)
        {

            var employee = unit.EmployeeRipository.GetById(id);
            return mapper.Map<Employee, EmployeeDTO>(employee);

        }

        public List<EmployeeDTO> GetAll()
        {
            var employeesData = unit.EmployeeRipository.GetAll();
            return mapper.Map<List<Employee>, List<EmployeeDTO>>(employeesData);
        }

        public bool UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            try
            {
                var existingEmployee = unit.EmployeeRipository.GetById(id);
                //app user
                if (existingEmployee == null)
                {
                    return false;
                }
                var existingEmployeeUser = unit.AppUserRepository.GetById(existingEmployee.UserId);

                mapper.Map(employeeDTO, existingEmployee);
                mapper.Map(employeeDTO, existingEmployeeUser);
                unit.EmployeeRipository.Update(existingEmployee);

                existingEmployeeUser.NormalizedUserName = userManager.NormalizeName(employeeDTO.userName);
                existingEmployeeUser.NormalizedEmail = userManager.NormalizeEmail(employeeDTO.email);
                unit.AppUserRepository.Update(existingEmployeeUser);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                var existingEmployee = unit.EmployeeRipository.GetById(id);
                var existingEmployeeUser = unit.AppUserRepository.GetById(existingEmployee.UserId);
                if (existingEmployee == null)
                {
                    return false;
                }
                existingEmployee.IsDeleted = true;
                existingEmployeeUser.IsDeleted = true;  
                unit.EmployeeRipository.Update(existingEmployee);
                unit.AppUserRepository.Update(existingEmployeeUser);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool ToggleActivityStatus(int id)
        {
            try
            {
                var existingEmployee = unit.EmployeeRipository.GetById(id);
                if (existingEmployee == null)
                    return false;
                existingEmployee.IsActive = !existingEmployee.IsActive;
                unit.EmployeeRipository.Update(existingEmployee);
                unit.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
    }
}
