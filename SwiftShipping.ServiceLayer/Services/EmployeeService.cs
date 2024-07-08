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
        private readonly UnitOfWork _unit;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper; 

        public EmployeeService(UnitOfWork unit, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<bool> addEmployeeAsync(EmployeeDTO employeeDTO)
        {
          
            var appUser = _mapper.Map<EmployeeDTO, ApplicationUser>(employeeDTO);

            IdentityResult result = await _userManager.CreateAsync(appUser, employeeDTO.password);
            if (result.Succeeded)
            {
                //Employee Role as String
                var EmployeeRole = RoleTypes.Employee.ToString();

                // check if the role is exist if not, add it
                if (await _roleManager.FindByNameAsync(EmployeeRole) == null)
                    await _roleManager.CreateAsync(new IdentityRole() { Name = EmployeeRole });

                // assign roles  to created user
                IdentityResult employeeRole = await _userManager.AddToRoleAsync(appUser, EmployeeRole);


                if (employeeRole.Succeeded)
                {
                    var employee = _mapper.Map<EmployeeDTO, Employee>(employeeDTO);
                    employee.UserId = appUser.Id;
                    _unit.EmployeeRipository.Insert(employee);
                    _unit.SaveChanges();
                    return true;
                }

            }

            return false;

        }

        
        public async Task<(bool Success, string UserId,string Role)> Login(LoginDTO loginDTO)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(loginDTO.email);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginDTO.userName);
            }

            if (user != null)
            {
                bool found = await _userManager.CheckPasswordAsync(user, loginDTO.password);
                if (found)
                {
                    await _signInManager.SignInAsync(user, loginDTO.RemembreMe);
                    // Fetch user roles
                    var roles = await _userManager.GetRolesAsync(user);
                    string role = roles.FirstOrDefault(); 

                    return (true, user.Id,role);
                }
            }

            return (false, null,null);
        }


        public EmployeeGetDTO GetById(int id)
        {

            var employee = _unit.EmployeeRipository.GetById(id);
            return _mapper.Map<Employee, EmployeeGetDTO>(employee);

        }
        public List<EmployeeGetDTO> GetAll()
        {
            var employeesData = _unit.EmployeeRipository.GetAll(employee => employee.IsDeleted == false);

            return _mapper.Map<List<Employee>, List<EmployeeGetDTO>>(employeesData);
        }

        public bool UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            try
            {
                var existingEmployee = _unit.EmployeeRipository.GetById(id);
                //app user
                if (existingEmployee == null)
                {
                    return false;
                }
                var existingEmployeeUser = _unit.AppUserRepository.GetById(existingEmployee.UserId);

                _mapper.Map(employeeDTO, existingEmployee);
                _mapper.Map(employeeDTO, existingEmployeeUser);
                _unit.EmployeeRipository.Update(existingEmployee);

                existingEmployeeUser.NormalizedUserName = _userManager.NormalizeName(employeeDTO.userName);
                existingEmployeeUser.NormalizedEmail = _userManager.NormalizeEmail(employeeDTO.email);
                _unit.AppUserRepository.Update(existingEmployeeUser);
                _unit.SaveChanges();
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
                var existingEmployee = _unit.EmployeeRipository.GetById(id);
                var existingEmployeeUser = _unit.AppUserRepository.GetById(existingEmployee.UserId);
                if (existingEmployee == null)
                {
                    return false;
                }
                existingEmployee.IsDeleted = true;
                existingEmployeeUser.IsDeleted = true;  
                _unit.EmployeeRipository.Update(existingEmployee);
                _unit.AppUserRepository.Update(existingEmployeeUser);
                _unit.SaveChanges();
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
                var existingEmployee = _unit.EmployeeRipository.GetById(id);
                if (existingEmployee == null)
                    return false;
                existingEmployee.IsActive = !existingEmployee.IsActive;
                _unit.EmployeeRipository.Update(existingEmployee);
                _unit.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
    }
}
