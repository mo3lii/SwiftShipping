﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryManController : ControllerBase
    {
        DeliveryManService deliveryManService;

        public DeliveryManController(DeliveryManService _deliveryManService)
        {
            deliveryManService = _deliveryManService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(DeliveryManDTO deliveryManDTO)
        {

            if (ModelState.IsValid)
            {
                await deliveryManService.AddDliveryManAsync(deliveryManDTO);

                return Ok("Delivery Man Added Successfully");
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("orders/{id}")]
        public ActionResult<List<OrderGetDTO>> GetDeliveryManOrders(int id)
        {
            var orders = deliveryManService.getDeliveryManOrders(id);
            return Ok(orders);
        }

        [HttpGet]
        public ActionResult<List<DeliveryManDTO>> GetAll()
        {
            var deliveryMen = deliveryManService.GetAll();
            return Ok(deliveryMen);
        }


        [HttpGet("{id}")]
        public ActionResult<DeliveryManDTO> GetById(int id)
        {
            var deliveryMan = deliveryManService.GetById(id);
            return Ok(deliveryMan);
        }


    }
}
