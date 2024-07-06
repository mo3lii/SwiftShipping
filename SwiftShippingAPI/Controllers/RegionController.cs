using AutoMapper;
using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;
using SwiftShipping.DataAccessLayer.Models;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly RegionService regionService;
        private readonly IMapper mapper;

        public RegionController(RegionService _regionService, IMapper _mapper)
        {
            regionService = _regionService;
            mapper = _mapper;
        }

        [HttpPost("Add")]
        public IActionResult Add(RegionDTO regionDTO)
        {
            regionService.Add(regionDTO);
            return Created();
        }

        [HttpGet("All")]
        [Authorize(Roles ="Employee")]
        public ActionResult<List<RegionGetDTO>> GetAll()
        {
            var regions = regionService.GetAll();
            return Ok(regions);
        }

        [HttpGet("{id}")]
        public ActionResult<RegionGetDTO> GetById(int id)
        {
            if (id == 0)
                return BadRequest(new ApiResponse(400));

            var region = regionService.GetById(id);

            if (region == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(region);
        }

        [HttpPut("Edit/{id}")]
        public IActionResult Edit(int id, RegionDTO regionDTO)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = regionService.EditRegion(id, regionDTO);
            if (!result) return NotFound(new ApiResponse(404));
            return Ok("Region Updated Successfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = regionService.DeleteRegion(id);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok("Region Deleted Successfully");
        }

        [HttpGet("Government/{id}")]
        public IActionResult getRegionsByGovermrntId(int id)
        {
            if (id == 0)
                return BadRequest(new ApiResponse(404));
            var regions = regionService.GetRegionsByGovernment(id);
            var res = mapper.Map<List<Region>, List<RegionGetDTO>>(regions);
            return Ok(res);

        }
    }
}
