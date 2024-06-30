
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RolePermissionService _rolePermissionService;

        public RoleController(RolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
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

