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

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
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

       

        [HttpGet("All")]
        //[Authorize(Policy = "CanView")]
        //[Authorize(Roles = "Employee,Admin")]

        public ActionResult<List<EmployeeDTO>> GetAll()
        {
            var employees = employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Employee")]

        public ActionResult<EmployeeDTO> GetById(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var employee = employeeService.GetById(id);

            if (employee == null) { return NotFound(new ApiResponse(404, "Employee does not exist")); }
            return Ok(employee);
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Employee")]

        public IActionResult UpdateEmployee(int id, EmployeeDTO employee)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = employeeService.UpdateEmployee(id, employee);
            if (!result) return NotFound(new ApiResponse(404, "Employee Does not exixt"));

            return Ok("Employee Updated Successfully");
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Employee")]

        public IActionResult DeleteEmployee(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = employeeService.DeleteEmployee(id);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok("Employee Deleted Successfully");
        }

        [HttpPut("ToggleActivityStatus/{id}")]
        [Authorize(Roles = "Employee")]

        public IActionResult ToggleActivityStatus(int id)
        {
            var res = employeeService.ToggleActivityStatus(id);
            if (res)
                return Ok("Activity Status Changed");
            else
                return BadRequest(new ApiResponse(400));
        }
    }
}
