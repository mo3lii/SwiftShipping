using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Authorization;
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
        private readonly BranchService _branchService;
        public BranchController(BranchService branchService) {
            _branchService = branchService;
        }

        [HttpGet("All")]
        //[Authorize(Policy = "Branches/View")]
        public IActionResult GetAllBranches()
        {
            var branches = _branchService.GetAll();
            return Ok(branches);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        //[Authorize(Policy = "Branches/Add")]
        public IActionResult addBranch(BranchDTO branchDTO)
        {
            _branchService.AddBrnach(branchDTO);
            return Ok(new ApiResponse(200,"Branch Created Successfully"));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        //[Authorize(Policy = "Branches/Edit")]
        public ActionResult<BranchGetDTO> GetById(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var branch = _branchService.GetById(id);

            if (branch == null) { return NotFound(new ApiResponse(404)); }
            return Ok(branch);
        }

        [HttpPut("Edit/{id}")]
        [Authorize(Roles = "Admin")]
        //[Authorize(Policy = "Branches/Edit")]
        public IActionResult EditBranch(int id, BranchDTO branchDTO)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _branchService.EditBranch(id, branchDTO);

            if (!result) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200,"Branch Updated Successfully"));
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        //[Authorize(Policy = "Branches/Delete")]
        public IActionResult DeleteBranch(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _branchService.DeleteBranch(id);

            if (!result) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, "Branch Deleted Successfully"));
        }



    }
}
