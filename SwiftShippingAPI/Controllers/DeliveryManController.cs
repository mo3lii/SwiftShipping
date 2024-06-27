using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryManController : ControllerBase
    {
        DeliveryManService deliveryManService;
        public DeliveryManController(DeliveryManService _deliveryManService)
        {
            deliveryManService = _deliveryManService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(DeliveryManDTO deliveryManDTO)
        {

            if (ModelState.IsValid)
            {
                await deliveryManService.AddDliveryManAsync(deliveryManDTO);


                return Ok("Delivery Man Added Successfully");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
