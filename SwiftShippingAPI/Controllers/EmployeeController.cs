using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Permissions;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;
using System.Security.Claims;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class EmployeeController : ControllerBase
    {
        UnitOfWork unitOfWork;
        EmployeeService employeeService;
        public EmployeeController(UnitOfWork _unitOfWork, EmployeeService _employeeService)
        {
            unitOfWork = _unitOfWork;
            this.employeeService = _employeeService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(EmployeeDTO employeeDTO)
        {

            if (ModelState.IsValid)
            {
                await employeeService.addEmployeeAsync(employeeDTO);


                return Ok("Employee Added Successfully");


            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
               var result =  await employeeService.Login(loginDTO);
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
                    return Created("Login Successfully", new {token = Token, role=result.Role});

                }
                else
                {
                    return BadRequest(new ApiResponse(400, "Login Faild"));
                }
            }
            else
            {
                return BadRequest(new ApiResponse(400)); 
            }    
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        //[Authorize(Policy = "CanView")]
        public ActionResult<List<EmployeeDTO>> GetAll()
        {
            var employees = employeeService.GetAll();

            if (employees.Count == 0) { return NotFound(new ApiResponse(404)); }

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeDTO> GetById(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var employee = employeeService.GetById(id);

            if ( employee == null) { return NotFound(new ApiResponse(404)); }
            return Ok(employee);
        }
    }
}
