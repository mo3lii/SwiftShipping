using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernmentController : ControllerBase
    {
        private readonly GovernmentService _governmentService;
        public GovernmentController(GovernmentService governmentService) {
            _governmentService = governmentService;
        }

        [HttpGet("All")]
        public ActionResult<List<GovernmentGetDTO>> GetAll()
        {
            var governemnts = _governmentService.GetAll();

            return Ok(governemnts);
        }

        [HttpPost("Add")]
        public IActionResult addGovernment(string name)
        {
            if (name == null) { return BadRequest(new ApiResponse(400)); }

            _governmentService.AddGovernment(name);
            return Created();
        }

        [HttpGet("{id}")]
        public ActionResult<GovernmentGetDTO> GetById(int id)
        {
            if (id == 0) { return BadRequest(new ApiResponse(400)); }

            var government = _governmentService.GetById(id);

            if (government == null) { return NotFound(new ApiResponse(404, "Government does not exist")); }

            return Ok(government);
        }

        [HttpPut("Edit/{id}")]
        public IActionResult Edit(int id, GovernmentDTO governmentDTO)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _governmentService.EditGovernment(id, governmentDTO);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, "Government Updated Successfully" ));
        }


        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _governmentService.Delete(id);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, "Government deleted Successfully"));

        }

        [HttpGet("GetAllGovernmentsWithRegions")]
        public ActionResult<List<GovernmentWithRegionsDTO>> GetAllGovernmentsWithRegions()
        {
            var governments = _governmentService.GetAllGovernmentsWithRegions();
            return Ok(governments);
        }

    }
}
