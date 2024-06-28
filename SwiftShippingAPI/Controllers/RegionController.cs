using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly RegionService regionService;
        public RegionController(RegionService _regionService)
        {
            regionService = _regionService;
        }

        [HttpGet]
        public ActionResult<List<RegionGetDTO>> GetAll()
        {
            var regions = regionService.GetAll();
            return Ok(regions);
        }
        [HttpPost("Add")]
        public IActionResult Add(RegionDTO regionDTO)
        {
            regionService.Add(regionDTO);
            return Created();
        }
    }
}
