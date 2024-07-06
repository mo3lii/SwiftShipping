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
        UnitOfWork unitOfWork;
        OrderService orderService;
        public OrderStatusController(UnitOfWork _unitOfWork, OrderService _orderService)
        {
            unitOfWork = _unitOfWork;
            orderService = _orderService;
        }

        [HttpGet("All")]
        public IActionResult GetAllStatuses()
        {
            return Ok(StatusMapper.StatusDictionary);
        }

        [HttpGet("getCount")] 
        public IActionResult getOrderStatusCount(OrderStatus status)
        {
             var res =  orderService.GetOrderStatusCount(status);
            return Ok(res);
        }

        [HttpGet("GetAllStatusCount")]
        public IActionResult GetAllStatusCount()
        {
           return Ok(orderService.GetAllOrderStatusCount());
        }


    }
}
