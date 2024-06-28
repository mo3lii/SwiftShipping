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

        [HttpPost]
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
    }
}
