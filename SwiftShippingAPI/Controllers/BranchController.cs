﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftShipping.ServiceLayer.DTO;
using SwiftShipping.ServiceLayer.Services;

namespace SwiftShipping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        BranchService branchService;
        public BranchController(BranchService _branchService) {
            branchService = _branchService;
        }


        [HttpPost("AddBranch")]
        public IActionResult addBranch(BranchDTO branchDTO)
        {

            branchService.AddBrnach(branchDTO);
            return Ok("Branch Created Successfully");
        }

        [HttpGet("GetAllBranches")]
        public IActionResult getAllBranches()
        {

            var branches =   branchService.GetAllBranches();
           return Ok(branches);

        }

    }
}
