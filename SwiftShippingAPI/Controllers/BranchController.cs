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

        [HttpGet]
        public IActionResult getAllBranches()
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
            var branch = branchService.GetById(id);
            return Ok(branch);
        }



    }
}
