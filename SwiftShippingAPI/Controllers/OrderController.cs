using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Enum;
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
        [HttpPost("Add")]
        public IActionResult Add(OrderDTO orderDTO)
        {
            orderService.AddOrder(orderDTO);
            return Created();
        }

        [HttpGet]
        public ActionResult<OrderGetDTO> GetAll() { 
        
            var orders = orderService.GetAll();

            if (orders.Count == 0) { return NotFound(new ApiResponse(404)); }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderGetDTO> GetById(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var order = orderService.GetById(id);

            if (order == null) { return NotFound(new ApiResponse(404)); }

            return Ok(order);
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

        [HttpGet("GetByStatus")]
        public IActionResult getByStatus(OrderStatus status)
        {
            var orders = orderService.GetByStatus(status);
            return Ok(orders);
        }

        [HttpGet("GetShippingType")]
        public IActionResult GetShippingTypes()
        {
            var shipingTypes = orderService.GetShippingTypes();
            return Ok(shipingTypes);
        }


        [HttpGet("GetShippingTime")]
        public IActionResult GetShippingTime()
        {
            var shipingTime = orderService.GetShippingTimes();
            return Ok(shipingTime);
        }

        [HttpPut("ChangeOrderStatus")]
        public IActionResult ChangeOrderStatus(OrderStatus status, int id )
        {
            var result = orderService.ChangeOrderStatus(id,status);
            if (result == true) 
            {
                return Ok("Status Changes Successfully");
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
