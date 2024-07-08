using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Permissions;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    public class RolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UnitOfWork _unit;
        private readonly IMapper _mapper;
        public RolesService(UnitOfWork unit,IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            _unit = unit;
            _mapper = mapper;
            _roleManager = roleManager;

        }


        public List<PermissionDTO> GetAllRolePermissions(string role)
        {
            var permissions =  _unit.RolePermissionsRepository.GetAll(r => r.RoleName == role);
            return  _mapper.Map<List<RolePermissions>,List<PermissionDTO>>(permissions);
        }
        public PermissionDTO GetPermissionsByDepartment(string role,Department department)
        {
            var permissions = _unit.RolePermissionsRepository.GetFirstByFilter(r => r.RoleName == role && r.DepartmentId==department );
            return _mapper.Map<RolePermissions, PermissionDTO>(permissions);
        }
        public bool isRolePermissionsExist(string role)
        {
            var rolepermissions = _unit.RolePermissionsRepository.GetAll(r => r.RoleName.ToLower() == role.ToLower());
            return rolepermissions.Any();
        }
        public bool updateRolePermissions(string role, List<PermissionDTO> permissionsDTO)
        {
            try
            {
                var rolepermissions = _unit.RolePermissionsRepository.GetAll(r => r.RoleName.ToLower() == role.ToLower()).Count();
                if (!isRolePermissionsExist(role)) return false;
                var rolePermissionsList = _mapper.Map<List<PermissionDTO>, List<RolePermissions>>(permissionsDTO);
                if (rolePermissionsList.Count()==0) return false;
                foreach (var item in rolePermissionsList)
                {
                    item.RoleName = role;
                    _unit.RolePermissionsRepository.Update(item);
                }
                _unit.SaveChanges();
                return true;
            }
            catch{return false;}
        }

        public async Task<IdentityRole> GetRole(string role)
        {
            var roleEntity = await _roleManager.FindByNameAsync(role);

            return roleEntity;
        }

        public async Task<bool> UpdateRole(string role, string updatedRole)
        {
            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity == null)
            {
                return false;
            }

            roleEntity.Name = updatedRole;
            roleEntity.NormalizedName = _roleManager.NormalizeKey(updatedRole);

            var result = await _roleManager.UpdateAsync(roleEntity);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> RoleExists(string role)
        {
            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity == null) return false;

            return true;
        }
    }
}
