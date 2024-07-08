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
        private readonly RegionService _regionService;
        private readonly IMapper _mapper;

        public RegionController(RegionService regionService, IMapper mapper)
        {
            _regionService = regionService;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public IActionResult Add(RegionDTO regionDTO)
        {
            _regionService.Add(regionDTO);
            return Ok(new ApiResponse(200, "Region Added Successfully"));
        }

        [HttpGet("All")]
        //[Authorize(Roles ="Employee,Admin")]
        public ActionResult<List<RegionGetDTO>> GetAll()
        {
            var regions = _regionService.GetAll();
            return Ok(regions);
        }

        [HttpGet("{id}")]
        public ActionResult<RegionGetDTO> GetById(int id)
        {
            if (id == 0)
                return BadRequest(new ApiResponse(400));

            var region = _regionService.GetById(id);

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

            var result = _regionService.EditRegion(id, regionDTO);

            if (!result) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, "Region Updated Successfully"));
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _regionService.DeleteRegion(id);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, "Region Deleted Successfully"));
        }

        [HttpGet("Government/{id}")]
        public IActionResult getRegionsByGovermrntId(int id)
        {
            if (id == 0)
                return BadRequest(new ApiResponse(404));
            var regions = _regionService.GetRegionsByGovernment(id);
            var res = _mapper.Map<List<Region>, List<RegionGetDTO>>(regions);
            return Ok(res);

        }
    }
}
