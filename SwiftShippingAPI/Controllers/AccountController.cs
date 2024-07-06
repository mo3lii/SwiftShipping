using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;
using System.Security.Claims;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AccountService _accountService;

        public AccountController(UserManager<ApplicationUser> userManager, AccountService accountService1)
        {
            _userManager = userManager;
            _accountService = accountService1;
        }

        [HttpGet("CheckEmail")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return Ok(new { exists = true });
            }

            return Ok(new { exists = false });
        }

        [HttpGet("CheckUsername")]
        public async Task<IActionResult> CheckUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                return Ok(new { exists = true });
            }

            return Ok(new { exists = false });
        }

        [HttpPost("LoginWithUserName")]
        public async Task<IActionResult> Login(LoginWithUserNameDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(loginDTO);

                if (result.Success == true)
                {
                    int Id = await _accountService.getIdByRole(result.UserId, result.Role);
                    // Create the claims
                    var claims = new List<Claim>
                    {
                        new Claim("UserId", result.UserId),
                        new Claim(ClaimTypes.Role, result.Role),
                        new Claim(ClaimTypes.NameIdentifier,Id.ToString())
                    };

                    var Token = JwtTokenHelper.GenerateToken(claims);
                    return Created("Login Successfully", new { token = Token,id=Id, role = result.Role });

                }
                else
                {
                    return NotFound(new ApiResponse(404, "Account does not exist"));
                }
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }

        [HttpPost("LoginWithEmail")]
        public async Task<IActionResult> LoginWithEmail(LoginWithEmailDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.LoginWithEmail(loginDTO);

                if (result.Success == true)
                {
                    int Id = await _accountService.getIdByRole(result.UserId, result.Role);

                    // Create the claims
                    var claims = new List<Claim>
                    {
                        new Claim("UserId", result.UserId),
                        new Claim(ClaimTypes.Role, result.Role),
                    };

                    var Token = JwtTokenHelper.GenerateToken(claims);
                    return Created("Login Successfully", new { token = Token, id = Id, role = result.Role });

                }

                    return NotFound(new ApiResponse(404, "Account does not exist"));

            }
            
                return BadRequest(new ApiResponse(400));
            
        }
    }
}

