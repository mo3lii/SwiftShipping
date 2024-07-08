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
        private readonly UnitOfWork _unitOfWork;
        private readonly OrderService _orderService;
        public OrderController(UnitOfWork unitOfWork, OrderService orderService)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
        }

        [HttpPost("Add")]
        public IActionResult Add(OrderDTO orderDTO)
        {
            _orderService.AddOrder(orderDTO);
            return Ok(new ApiResponse(200, "order added successfully"));
        }

        [HttpGet("All")]
        public ActionResult<OrderGetDTO> GetAll() { 
        
            var orders = _orderService.GetAll();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderGetDTO> GetById(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var order = _orderService.GetById(id);

            if (order == null) { return NotFound(new ApiResponse(404, "Order Not Fond")); }

            return Ok(order);
        }

        [HttpPost("AssignToDeliveryMan")]
        public async Task<IActionResult> AssignDeliveryManToOrder(int orderID,  int deliveryManID )
        {
            var result = _orderService.AssignOrderToDeliveryMan(orderID, deliveryManID);
            if(result == true)
            {
                return Ok(new ApiResponse(200, "Order Assigned Successfully to Delivery man"));
            }
           
             return BadRequest(new ApiResponse(400, "Fail To assign order to Delivery Man"));
          

        }

        [HttpGet("GetByStatus")]
        public IActionResult getByStatus(OrderStatus status)
        {
            if (status == null) return BadRequest(new ApiResponse(400));

            var orders = _orderService.GetByStatus(status);
            return Ok(orders);
        }

        [HttpGet("OrderTypes")]
        public IActionResult GetOrderTypes()
        {
            var ordersTypes = _orderService.GetOrderTypes();
            return Ok(ordersTypes);
        }

        [HttpGet("ShippingTypes")]
        public IActionResult GetShippingTypes()
        {
            var shipingTypes = _orderService.GetShippingTypes();
            return Ok(shipingTypes);
        }

        [HttpGet("PaymentTypes")]
        public IActionResult GetPaymentTypes()
        {
            var PaymentTypes = _orderService.GetPaymentTypes();
            return Ok(PaymentTypes);
        }
        [HttpPut("ChangeOrderStatus/{id}")]
        public IActionResult ChangeOrderStatus(int id, [FromBody] OrderStatus status)
        {
            var result = _orderService.ChangeOrderStatus(status, id);

            if (result)
            {
                return Ok(new ApiResponse(200, "Status changed successfully" ));
            }

            return BadRequest(new ApiResponse(400, "Failed to change status"));
        }

        [HttpPut("Edit/{id}")]
        public IActionResult Edit(int id, OrderDTO order)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _orderService.UpdateOrder(id, order);

            if (!result) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, "Order Updated Successfully"));
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            if (id == 0) return BadRequest(new ApiResponse(400));

            var result = _orderService.DeleteOrder(id);
            if (!result) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, "Order Deleted Successfully"));
        }

        [HttpPost("OrderCost")]
        public IActionResult OrderCost(OrderCostDTO order)
        {
            var orderCost = _orderService.CalculateOrderCost(order);
            return Ok(orderCost);
        }
    }
}
