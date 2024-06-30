﻿using E_CommerceAPI.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernmentController : ControllerBase
    {
        private readonly GovernmentService governmentService;
        public GovernmentController(GovernmentService _governmentService) {
            governmentService = _governmentService;
        }

        [HttpGet]
        public ActionResult<List<GovernmentGetDTO>> GetAll()
        {
            var governemnts = governmentService.GetAll();

            if (governemnts.Count == 0) { return NotFound(new ApiResponse(404)); }

            return Ok(governemnts);
        }

        [HttpPost("Add")]
        public IActionResult addGovernment(string name)
        {
            if (name == null) { return BadRequest(new ApiResponse(400)); }

            governmentService.AddGovernment(name);
            return Created();
        }

        [HttpGet("{id}")]
        public ActionResult<GovernmentGetDTO> GetById(int id)
        {
            if (id == 0) { return BadRequest(new ApiResponse(400)); }

            var government = governmentService.GetById(id);

            if (government == null) { return NotFound(new ApiResponse(404)); }


            return Ok(government);
        }


    }
}
