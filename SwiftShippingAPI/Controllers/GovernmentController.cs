using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("AddGovernment")]
        public IActionResult addGovernment(string name, bool isActive = true)
        {

            governmentService.AddGovernment(name, isActive);
            return Ok("Government Created Successfully");
        }
    }
}
