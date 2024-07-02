using AutoMapper;
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
        UnitOfWork unit;
        IMapper mapper;
        public RolesService(UnitOfWork _unit,IMapper _mapper)
        {
            unit = _unit;
            mapper = _mapper;
        }
        public List<PermissionDTO> GetAllRolePermissions(string role)
        {

            var permissions =  unit.RolePermissionsRepository.GetAll(r => r.RoleName == role);
            return  mapper.Map<List<RolePermissions>,List<PermissionDTO>>(permissions);
        }
        public bool isRolePermissionsExist(string role)
        {
            var rolepermissions = unit.RolePermissionsRepository.GetAll(r => r.RoleName.ToLower() == role.ToLower());
            return rolepermissions.Any();
        }
        public bool updateRolePermissions(string role, List<PermissionDTO> permissionsDTO)
        {
            try
            {
                var rolepermissions = unit.RolePermissionsRepository.GetAll(r => r.RoleName.ToLower() == role.ToLower()).Count();
                if (!isRolePermissionsExist(role)) return false;
                var rolePermissionsList = mapper.Map<List<PermissionDTO>, List<RolePermissions>>(permissionsDTO);
                if (rolePermissionsList.Count()==0) return false;
                foreach (var item in rolePermissionsList)
                {
                    item.RoleName = role;
                    unit.RolePermissionsRepository.Update(item);
                }
                unit.SaveChanges();
                return true;
            }
            catch{return false;}
        }
    }
}
