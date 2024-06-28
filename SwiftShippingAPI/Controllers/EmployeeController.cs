using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                return BadRequest();
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
                        new Claim(ClaimTypes.Role, result.Role)
                    };

                    var Token = JwtTokenHelper.GenerateToken(claims);
                    return Created("Login Successfully", new {token = Token, role=result.Role});

                }
                else
                {
                    return BadRequest("Login Failed");
                }
            }
            else
            {
                return BadRequest(); 
            }    
        }

        [HttpGet]
        public ActionResult<List<EmployeeDTO>> GetAll()
        {
            var employees = employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeDTO> GetById(int id)
        {
            var employee = employeeService.GetById(id);
            return Ok(employee);
        }
    }
}
