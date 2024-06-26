﻿using AutoMapper;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    public class BranchService
    {
        private UnitOfWork unit;
        private readonly IMapper mapper;

        public BranchService(UnitOfWork _unit, IMapper _mapper)
        {
            unit = _unit;
            mapper = _mapper;
        }

        public bool AddBrnach(BranchDTO branchDTO)
        {
            try
            {
                var branch = mapper.Map<BranchDTO,Branch>(branchDTO);
               branch.CreationDate = DateTime.Now;
                unit.BranchRipository.Insert(branch);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    
        public List<BranchGetDTO> GetAll()
        {
            var branches = unit.BranchRipository.GetAll();
            return mapper.Map<List<Branch>, List<BranchGetDTO>>(branches);
        }

        public BranchGetDTO GetById(int id)
        {
            var branch = unit.BranchRipository.GetById(id);
            return mapper.Map<Branch, BranchGetDTO>(branch);

        }

    }
}
