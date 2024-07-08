
using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RolesService _rolesService;
        public RoleController( RolesService rolesService)
        {

            _rolesService = rolesService;
        }

        [HttpGet("{role}")]
        public ActionResult<List<RolePermissions>> GetPermissionsByRole(string role){
            var PermissionsDTOList = _rolesService.GetAllRolePermissions(role);
            if(PermissionsDTOList == null || PermissionsDTOList.Count()==0) return NotFound(new ApiResponse(404));
            return Ok(PermissionsDTOList);
        }
        [HttpGet("Department")]
        public ActionResult<RolePermissions> GetDepartmentPermissionsByRole(string role,Department department)
        {
            var PermissionsDTO = _rolesService.GetPermissionsByDepartment(role,department);
            if (PermissionsDTO == null) return NotFound(new ApiResponse(404));
            return Ok(PermissionsDTO);
        }

        [HttpPut("{role}")]
        public ActionResult<List<RolePermissions>> updatePermissionsByRole(string role,[FromBody]List<PermissionDTO> permissionsDTOList)
        {
            var result = _rolesService.updateRolePermissions(role, permissionsDTOList);
            if (!result) return NotFound(new ApiResponse(404));
            return Ok("updated successfully");
        }


        [HttpGet("Get/{role}")]
        public async Task<ActionResult<IdentityRole>> GetRole(string role)
        {
            var roleEntity = await _rolesService.GetRole(role);

            if (roleEntity == null)
            {
                return NotFound();
            }

            return roleEntity;
        }

        [HttpPut("Update/{role}")]
        public async Task<IActionResult> UpdateRole(string role, string updatedRole)
        {
            if (role == updatedRole)
            {
                return BadRequest(new ApiResponse(400));
            }

            var result = await _rolesService.UpdateRole(role, updatedRole);

            if (!result) return BadRequest(new ApiResponse(400, "Faild To Update"));

            return NoContent();
        }

        [HttpGet("Exist")]
        public async Task<ActionResult<bool>> RoleExists(string role)
        {
            return await _rolesService.RoleExists(role);
        }
    }

    public class AssignPermissionsToRoleDto
    {
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class RemovePermissionFromRoleDto
    {
        public string RoleName { get; set; }
        public string Permission { get; set; }
    }
}

