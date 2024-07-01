
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RolePermissionService _rolePermissionService;
        private readonly RolesService _rolesService;
        public RoleController(RolePermissionService rolePermissionService, RolesService rolesService)
        {
            _rolePermissionService = rolePermissionService;
            _rolesService = rolesService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignPermissionsToRole([FromBody] AssignPermissionsToRoleDto model)
        {
            await _rolePermissionService.AssignPermissionsToRoleAsync(model.RoleName, model.Permissions);
            return Ok();
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemovePermissionFromRole([FromBody] RemovePermissionFromRoleDto model)
        {
            await _rolePermissionService.RemovePermissionFromRoleAsync(model.RoleName, model.Permission);
            return Ok();
        }

        [HttpGet("{role}")]
        public ActionResult<List<RolePermissions>> GetPermissionsByRole(string role){
            var PermissionsDTOList = _rolesService.GetAllRolePermissions(role);
            if(PermissionsDTOList == null || PermissionsDTOList.Count()==0) return NotFound();
            return Ok(PermissionsDTOList);
        }

        [HttpPut("{role}")]
        public ActionResult<List<RolePermissions>> updatePermissionsByRole(string role,[FromBody]List<PermissionDTO> permissionsDTOList)
        {
            var result = _rolesService.updateRolePermissions(role, permissionsDTOList);
            if (!result) return NotFound();
            return Ok("updated successfully");
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

