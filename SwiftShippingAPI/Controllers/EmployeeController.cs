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
        private readonly UnitOfWork _unitOfWork;
        private readonly EmployeeService _employeeService;
        public EmployeeController(UnitOfWork unitOfWork, EmployeeService employeeService)
        {
            _unitOfWork = unitOfWork;
            _employeeService = employeeService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Register(EmployeeDTO employeeDTO)
        {

            if (ModelState.IsValid)
            {
                await _employeeService.addEmployeeAsync(employeeDTO);
                return Ok(new ApiResponse(200, "Employee add Successfully"));
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }


        

        [HttpGet("All")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<List<EmployeeGetDTO>> GetAllEmployees()
        {
            var employees = _employeeService.GetAll();
            return Ok(employees);
        }


        [HttpGet("{id}")]

        public ActionResult<EmployeeGetDTO> GetById(int id)

        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var employee = _employeeService.GetById(id);

            if (employee == null) { return NotFound(new ApiResponse(404, "Employee does not exist")); }
            return Ok(employee);
        }

        [HttpPut("Update/{id}")]

        public IActionResult UpdateEmployee(int id, EmployeeDTO employee)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _employeeService.UpdateEmployee(id, employee); 
            if (!result) return NotFound(new ApiResponse(404, "Employee Does not exixt"));

            return Ok(new ApiResponse(200, "Employee Updated Successfully" ));
        }

        [HttpDelete("Delete/{id}")]
        //[Authorize(Roles = "Employee")]

        public IActionResult DeleteEmployee(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _employeeService.DeleteEmployee(id);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, " Employee Deleted Successfully"));
        }

        [HttpPut("ToggleActivityStatus/{id}")]
        //[Authorize(Roles = "Employee")]

        public IActionResult ToggleActivityStatus(int id)
        {
            var res = _employeeService.ToggleActivityStatus(id);
            if (res)
                return Ok(new ApiResponse(200, "Activity Status Changed" ));
            else
                return BadRequest(new ApiResponse(400));
        }
    }
}
