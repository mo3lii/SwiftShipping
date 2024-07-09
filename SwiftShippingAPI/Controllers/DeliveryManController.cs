using AutoMapper;
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
        private readonly DeliveryManService _deliveryManService;
        private readonly RegionService _regionService;
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;

        public DeliveryManController(DeliveryManService deliveryManService,
            RegionService regionService,
            OrderService orderService, IMapper mapper)
        {
            _deliveryManService = deliveryManService;
            _regionService = regionService;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Register(DeliveryManDTO deliveryManDTO)
        {

            if (ModelState.IsValid)
            {
               var result= await _deliveryManService.AddDliveryManAsync(deliveryManDTO);

                return Ok(new { message = "Delivery Man Added Successfully", deliverymanId=result.deliveryManId });
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
                var result = await _deliveryManService.Login(loginDTO);

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


        [HttpPost("AssignRegions")]
        public async Task<IActionResult> AssignRegionsToDeliveryMan([FromBody] AssignRegionsDTO assignRegionsDTO)
        {
            var result = _deliveryManService.AssignRegionsToDeliveryMan(assignRegionsDTO.DeliveryManId, assignRegionsDTO.RegionIds);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }


        //[HttpPost("AssignToRegion")]
        //public IActionResult AssignToRegion(int deliveryManId, int regionId)
        //{
        //    if (deliveryManId == 0 || regionId == 0) return BadRequest(new ApiResponse(400));

        //    var deliveryMan = _deliveryManService.GetById(deliveryManId);

        //    if (deliveryMan == null) return NotFound(new ApiResponse(404, "Delivary man does not exist"));

        //    var region = _regionService.GetById(regionId);

        //    if (region == null) return NotFound(new ApiResponse(404, "Redion does not exist"));

        //    var result = _deliveryManService.assignDeliveryManTORegion(deliveryManId, regionId);

        //    if (result) return Ok(new ApiResponse(200, "region assigned successfully"));

        //    return BadRequest(new ApiResponse(400));
        //}

        [HttpGet("{id}/orders")]
        public ActionResult<List<OrderGetDTO>> GetDeliveryManOrders(int id, OrderStatus? status = null)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var deliveryMan = _deliveryManService.GetById(id);

            if (deliveryMan == null) { return NotFound(new ApiResponse(404, "Delivary man does not exist")); } 

            var orders = _deliveryManService.GetDeliveryManOrders(id, status);

            return Ok(orders);
        }

        [HttpGet("All")]
        public ActionResult<List<DeliveryManGetDTO>> GetAll()
        {
            var deliveryMen = _deliveryManService.GetAll();

            return Ok(deliveryMen);
        }


        [HttpGet("{id}")]
        public ActionResult<DeliveryManGetDTO> GetById(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var deliveryMan = _deliveryManService.GetById(id);

            if (deliveryMan == null) {  return NotFound(new ApiResponse(404, "Delivary man does not exist")); }

            return Ok(deliveryMan);
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateDeliveryMan(int id, DeliveryManDTO deliveryManDTO)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _deliveryManService.UpdateDeliveryMan(id, deliveryManDTO);
            if (!result) return NotFound(new ApiResponse(404, "delivary Does not exixt"));

            return Ok(new ApiResponse(200, " Delivery Man Updated Successfully"));
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteDeliveryMan(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _deliveryManService.DeleteDeliveryMan(id);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, " Delivery Man Deleted Successfully" ));
        }

        [HttpPost("AssignRegions/{deliveryManId}")]
        public IActionResult AssignRegions(int deliveryManId, int[] regionsId)
        {
            var res = _deliveryManService.AssignRegionsToDeliveryMan(deliveryManId, regionsId);
            if (res == true)
                return Ok(new ApiResponse(200, "regions assigned successfully"));

            return BadRequest(new ApiResponse(400));
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

        [HttpGet("DeliveryManRegions/{deliveryManId}")]
        public IActionResult GetDeliveryManRegions(int deliveryManId)
        {
            List<DeliveryManRegions> res =  _deliveryManService.GetDeliveryManRegions(deliveryManId);

            var regions = _mapper.Map<List<DeliveryManRegions>, List<RegionGetDTO>>(res);

            return Ok(regions);

        }
    }
}
