using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernmentController : ControllerBase
    {
        private readonly GovernmentService governmentService;
        public GovernmentController(GovernmentService _governmentService) {
            governmentService = _governmentService;
        }

        [HttpGet]
        public ActionResult<List<GovernmentGetDTO>> GetAll()
        {
            var governemnts = governmentService.GetAll();
            return Ok(governemnts);
        }

        [HttpPost("Add")]
        public IActionResult addGovernment(string name)
        {
            governmentService.AddGovernment(name);
            return Created();
        }

        [HttpGet("{id}")]
        public ActionResult<GovernmentGetDTO> GetById(int id)
        {
            var government = governmentService.GetById(id);
            return Ok(government);
        }


    }
}
