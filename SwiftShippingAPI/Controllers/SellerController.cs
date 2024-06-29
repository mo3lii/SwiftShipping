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
    }
}
