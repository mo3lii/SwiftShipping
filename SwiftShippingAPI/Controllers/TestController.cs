using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        SellerService sellerService;

        public TestController(SellerService _sellerService) {

            sellerService = _sellerService;
        }

        [HttpPost("addSeller")]
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
