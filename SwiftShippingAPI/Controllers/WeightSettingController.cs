using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightSettingController : ControllerBase
    {
        private readonly WeightSettingService _weightSettingService;
        public WeightSettingController(WeightSettingService weightSettingService)
        {
            _weightSettingService = weightSettingService;
        }

        [HttpGet]
        public async Task<ActionResult<WeightSetting>> GetWeightSetting()
        {
            var weightSetting = await _weightSettingService.GetSettingAsync();

            if (weightSetting == null)
            {
                return NotFound();
            }

            return weightSetting;
        }

        [HttpPut]
        public async Task<IActionResult> PutWeightSetting([FromBody] WeightSetting updatedWeightSetting)
        {
            var updateResult = await _weightSettingService.UpdateSetting(updatedWeightSetting);

            if (!updateResult)
            {
                return NotFound();
            }

            return Ok(new ApiResponse(200, "Updated Successfully"));
        }
    }
}
