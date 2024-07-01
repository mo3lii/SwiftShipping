using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private SellerService sellerService; 
        public SellerController(SellerService sellerService)
        {
            this.sellerService = sellerService;

        }

        [HttpPost("Add")]
        public async Task<IActionResult> addSeller(SellerDTO sellerDTO)
        {
            if (ModelState.IsValid)
            {
                await sellerService.addSellerAsync(sellerDTO);
                return Ok("seller Added Successfully");
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public ActionResult<SellerGetDTO> GetById(int id)
        {
            var seller = sellerService.GetById(id);
            if (seller == null) return NotFound();
            return Ok(seller);
        }

        [HttpGet]
        public ActionResult<List<SellerGetDTO>> GetAll()
        {
            var sellers = sellerService.GetAll();
            return Ok(sellers);
        }
        [HttpGet("{id}/orders")]
        public ActionResult<List<OrderGetDTO>> getSellerOrders(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400,"seller not exist"));
            var orders = sellerService.GetSellerOrders(id);
            if (orders.Count == 0) return NotFound(new ApiResponse(404, "This Seller has no orders"));
            return Ok(orders);
        }

        [HttpPut("Edit/{id}")]
        public IActionResult Edit(int id, SellerDTO sellerDTO)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = sellerService.Update(id, sellerDTO);
            if (!result) return NotFound(new ApiResponse(404));
            return Ok("Seller Updated Successfully");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = sellerService.Delete(id);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok("Seller Deleted Successfully");
        }

    }
}
