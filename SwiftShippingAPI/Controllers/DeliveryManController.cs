using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;
using System.Security.Claims;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryManController : ControllerBase
    {
        DeliveryManService deliveryManService;
        RegionService regionService;
        private OrderService _orderService;
        public DeliveryManController(DeliveryManService _deliveryManService,
            RegionService _regionService,
            OrderService orderService)
        {
            deliveryManService = _deliveryManService;
            regionService = _regionService;
            _orderService = orderService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Register(DeliveryManDTO deliveryManDTO)
        {

            if (ModelState.IsValid)
            {
                await deliveryManService.AddDliveryManAsync(deliveryManDTO);

                return Ok("Delivery Man Added Successfully");
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await deliveryManService.Login(loginDTO);

                if (result.Success == true)
                {
                    // Create the claims
                    var claims = new List<Claim>
                    {
                       new Claim("UserId", result.UserId),
                       new Claim(ClaimTypes.Role, result.Role),
                       //new Claim("Policy", "CanView")
                    };

                    var Token = JwtTokenHelper.GenerateToken(claims);
                    return Created("Login Successfully", new { token = Token, role = result.Role });

                }

                return NotFound(new ApiResponse(404, "Delivary Man does not exist"));
            }

            return BadRequest(new ApiResponse(400, "Login Faild"));
        }

        [HttpPost("AssignToRegion")]
        public IActionResult AssignToRegion(int deliveryManId, int regionId)
        {
            if (deliveryManId == 0 || regionId == 0) return BadRequest(new ApiResponse(400));

            var deliveryMan = deliveryManService.GetById(deliveryManId);

            if (deliveryMan == null) return NotFound(new ApiResponse(404, "Delivary man does not exist"));

            var region = regionService.GetById(regionId);

            if (region == null) return NotFound(new ApiResponse(404, "Redion does not exist"));

            var result = deliveryManService.assignDeliveryManTORegion(deliveryManId, regionId);

            if (result) return Ok();

            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("{id}/orders")]
        public ActionResult<List<OrderGetDTO>> GetDeliveryManOrders(int id, OrderStatus? status = null)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var deliveryMan = deliveryManService.GetById(id);

            if (deliveryMan == null) { return NotFound(new ApiResponse(404, "Delivary man does not exist")); } 

            var orders = deliveryManService.GetDeliveryManOrders(id, status);

            return Ok(orders);
        }

        [HttpGet("All")]
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

            if (deliveryMan == null) {  return NotFound(new ApiResponse(404, "Delivary man does not exist")); }

            return Ok(deliveryMan);
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateDeliveryMan(int id, DeliveryManDTO deliveryManDTO)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = deliveryManService.UpdateDeliveryMan(id, deliveryManDTO);
            if (!result) return NotFound(new ApiResponse(404, "delivary Does not exixt"));

            return Ok("Delivery Man Updated Successfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteDeliveryMan(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = deliveryManService.DeleteDeliveryMan(id);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok("Delivery Man Deleted Successfully");
        }

        [HttpGet("Count")]
        public IActionResult getOrderStatusCount(OrderStatus status, int delivaryId)
        {
            var res = _orderService.GetOrderStatusCountForSeller(status, delivaryId);
            return Ok(res);
        }

        [HttpGet("AllStatusCount/{delivaryId}")]
        public IActionResult GetAllStatusCount(int delivaryId)
        {
            return Ok(_orderService.GetAllOrderStatusCountForSeller(delivaryId));
        }

    }
}
