using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        UnitOfWork unitOfWork;
        OrderService orderService;
        public OrderController(UnitOfWork _unitOfWork, OrderService _orderService)
        {
            unitOfWork = _unitOfWork;
            orderService = _orderService;
        }

        [HttpPost("AssignToDeliveryMan")]
        public async Task<IActionResult> AssignDeliveryManToOrder(int orderID,  int deliveryManID )
        {
            var result = orderService.AssignOrderToDeliveryMan(orderID, deliveryManID);
            if(result == true)
            {
                return Ok("Order Assigned Successfully to Delivery man");
            }
            else
            {
                return BadRequest("Fail To assign order to Delivery Man");
            }

        }

            

    }
}
