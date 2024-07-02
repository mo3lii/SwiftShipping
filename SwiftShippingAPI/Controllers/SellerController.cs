using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;
using System.Security.Claims;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private SellerService _sellerService; 
        private OrderService _orderService;
        public SellerController(SellerService sellerService, OrderService orderService)
        {
            _sellerService = sellerService;
            _orderService = orderService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _sellerService.Login(loginDTO);
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
                else
                {
                     return NotFound(new ApiResponse(404, "Seller does not exist"));
                }
            }
            else
            {
                 return BadRequest(new ApiResponse(400, "Login Faild"));
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> addSeller(SellerDTO sellerDTO)
        {
            if (ModelState.IsValid)
            {
                await _sellerService.addSellerAsync(sellerDTO);
                return Ok("seller Added Successfully");
            }
           
                return BadRequest(new ApiResponse(400)); 
        }


        [HttpGet("{id}")]
        public ActionResult<SellerGetDTO> GetById(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var seller = _sellerService.GetById(id);
            if (seller == null) return NotFound();
            return Ok(seller);
        }

        [HttpGet]
        public ActionResult<List<SellerGetDTO>> GetAll()
        {
            var sellers = _sellerService.GetAll();
            return Ok(sellers);
        }

        [HttpGet("{id}/orders")]
        public ActionResult<List<OrderGetDTO>> getSellerOrders(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400,"seller not exist"));

            var orders = _sellerService.GetSellerOrders(id);

            return Ok(orders);
        }

        [HttpPut("Edit/{id}")]
        public IActionResult Edit(int id, SellerDTO sellerDTO)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _sellerService.Update(id, sellerDTO);
            if (!result) return NotFound(new ApiResponse(404));
            return Ok("Seller Updated Successfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _sellerService.Delete(id);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok("Seller Deleted Successfully");
        }

        [HttpGet("Count")]
        public IActionResult getOrderStatusCount(OrderStatus status, int sellerId)
        {
            var res = _orderService.GetOrderStatusCountForSeller(status, sellerId);
            return Ok(res);
        }

        [HttpGet("AllStatusCount/{SellerId}")]
        public IActionResult GetAllStatusCount(int SellerId)
        {
            return Ok(_orderService.GetAllOrderStatusCountForSeller(SellerId));
        }

    }
}
