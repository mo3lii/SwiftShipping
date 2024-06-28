using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        SellerService sellerService;
        DeliveryManService deliveryManService;
        OrderService orderService;
        GovernmentService governmentService;
        RegionService regionService;

        private readonly IMapper _mapper;
        private readonly UnitOfWork unit;
        public TestController(SellerService _sellerService, DeliveryManService _deliveryManService, OrderService _orderService
            , GovernmentService _governmentService, RegionService _regionService,
            IMapper mapper,UnitOfWork unit)
        {

            sellerService = _sellerService;
            deliveryManService = _deliveryManService;
            orderService = _orderService;
            this.governmentService = _governmentService;
            regionService = _regionService;
            _mapper = mapper;
            this.unit = unit;

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
        [HttpPost("addDeliveryMan")]
        public async Task<IActionResult> addDeliveryMan(DeliveryManDTO deliveryMan)
        {

            if (ModelState.IsValid)
            {
                await deliveryManService.AddDliveryManAsync(deliveryMan);

                return Ok("delivery Man Added Successfully");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("addOrder")]
        public async Task<IActionResult> addOrder(OrderDTO orderData)
        {

            if (ModelState.IsValid)
            {

                 orderService.AddOrder(orderData);
                 return Ok("Order Added Successfully");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("addGovernment")]
        public async Task<IActionResult> addGovernment(string name)
        {

            if (ModelState.IsValid)
            {

                governmentService.AddGovernment(name);
                return Ok("Government Added Successfully");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("addRegion")]
        public async Task<IActionResult> addRegion(RegionDTO regionDTO)
        {

            if (ModelState.IsValid)
            {

               regionService.Add(regionDTO);
                return Ok("Region Added Successfully");
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("testGetById")]
        public IActionResult TestGetById()
        {
            //var deliveryRegions = unit.DeliveryManRegionsRipository.GetById(5, 1);
            var user = unit.AppUserRepository.GetById("b8e197ce-0bc5-4aaf-9212-e42ef29b6bc8");
            return Ok(new {Name= user.Name });
        }

    }

}
