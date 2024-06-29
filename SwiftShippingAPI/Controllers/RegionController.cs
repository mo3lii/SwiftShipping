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

        [HttpPost("Add")]
        public IActionResult Add(RegionDTO regionDTO)
        {
            regionService.Add(regionDTO);
            return Created();
        }

        [HttpGet]
        public ActionResult<List<RegionGetDTO>> GetAll()
        {
            var regions = regionService.GetAll();
            return Ok(regions);
        }

        [HttpGet("{id}")]
        public ActionResult<RegionGetDTO> GetById(int id)
        {
            var region = regionService.GetById(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }

    }
}
