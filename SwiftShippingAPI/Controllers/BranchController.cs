using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        BranchService branchService;
        public BranchController(BranchService _branchService) {
            branchService = _branchService;
        }

        [HttpGet("All")]
        public IActionResult GetAllBranches()
        {
            var branches = branchService.GetAll();
            return Ok(branches);
        }

        [HttpPost("Add")]
        public IActionResult addBranch(BranchDTO branchDTO)
        {
            branchService.AddBrnach(branchDTO);
            return Ok("Branch Created Successfully");
        }

        [HttpGet("{id}")]
        public ActionResult<BranchGetDTO> GetById(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var branch = branchService.GetById(id);

            if (branch == null) { return NotFound(new ApiResponse(404)); }
            return Ok(branch);
        }

        [HttpPut("Edit/{id}")]
        public IActionResult EditBranch(int id, BranchDTO branchDTO)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = branchService.EditBranch(id, branchDTO);

            if (!result) return NotFound(new ApiResponse(404));

            return Ok("Branch Updated Successfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteBranch(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = branchService.DeleteBranch(id);

            if (!result) return NotFound(new ApiResponse(404));

            return Ok("Branch Deleted Successfully");
        }



    }
}
