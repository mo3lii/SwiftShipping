using E_CommerceAPI.Errors;
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
        RegionService regionService;
        public DeliveryManController(DeliveryManService _deliveryManService,RegionService _regionService)
        {
            deliveryManService = _deliveryManService;
            regionService = _regionService;
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
        [HttpPost("AssignToRegion")]
        public IActionResult AssignToRegion(int deliveryManId,int regionId)
        {
            var deliveryMan = deliveryManService.GetById(deliveryManId);
            if (deliveryMan == null) return NotFound();
            var region = regionService.GetById(regionId);
            if (region == null) return NotFound();
            var result = deliveryManService.assignDeliveryManTORegion(deliveryManId, regionId);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpGet("{id}/orders")]
        public ActionResult<List<OrderGetDTO>> GetDeliveryManOrders(int id)
        {
            var deliveryMan = deliveryManService.GetById(id);
            if (deliveryMan == null) { return NotFound(); } 
            var orders = deliveryManService.getDeliveryManOrders(id);
            return Ok(orders);
        }

        [HttpGet]
        public ActionResult<List<DeliveryManGetDTO>> GetAll()
        {
            var deliveryMen = deliveryManService.GetAll();
            return Ok(deliveryMen);
        }


        [HttpGet("{id}")]
        public ActionResult<DeliveryManGetDTO> GetById(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var deliveryMan = deliveryManService.GetById(id);
            if (deliveryMan == null) {  return NotFound(new ApiResponse(404)); }
            return Ok(deliveryMan);
        }


    }
}
