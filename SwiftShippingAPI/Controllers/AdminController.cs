using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;
using System.Security.Claims;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginWithUserNameDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await  _adminService.Login(loginDTO);

                if (result.Success == true)
                {
                    // Create the claims
                    var claims = new List<Claim>
                    {
                        new Claim("UserId", result.UserId),
                        new Claim(ClaimTypes.Role, result.Role),
                        //new Claim("Policy", "CanView")
                    };

                    var Token = JwtTokenHelper.GenerateToken(claims);
                    return Created("Login Successfully", new { token = Token, role = result.Role });

                }
                else
                {
                    return NotFound(new ApiResponse(404, "Admin does not exist"));
                }
            }
            else
            {
                  return BadRequest(new ApiResponse(400));
            }
        }
    }
}
