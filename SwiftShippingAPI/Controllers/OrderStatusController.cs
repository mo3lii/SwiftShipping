using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.Helper;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderStatusController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("All")]
        public IActionResult GetAllStatuses()
        {
            return Ok(StatusMapper.StatusDictionary);
        }

        [HttpGet("getCount")] 
        public IActionResult getOrderStatusCount(OrderStatus status)
        {
             var res =  _orderService.GetOrderStatusCount(status);
            return Ok(res);
        }

        [HttpGet("GetAllStatusCount")]
        public IActionResult GetAllStatusCount()
        {
           return Ok(_orderService.GetAllOrderStatusCount());
        }


    }
}
