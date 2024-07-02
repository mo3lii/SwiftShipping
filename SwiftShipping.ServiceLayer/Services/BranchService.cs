using AutoMapper;
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
    
        public IEnumerable<BranchGetDTO> GetAll()
        {
            var branches = unit.BranchRipository.GetAll();
            return mapper.Map<IEnumerable<Branch>, IEnumerable<BranchGetDTO>>(branches);
        }

        public BranchGetDTO GetById(int id)
        {
            var branch = unit.BranchRipository.GetById(id);
            return mapper.Map<Branch, BranchGetDTO>(branch);

        }


        public bool EditBranch(int id, BranchDTO branchDTO)
        {
            try
            {
                var existingBranch = unit.BranchRipository.GetById(id);

                if (existingBranch == null)
                {
                    return false;
                }

                mapper.Map(branchDTO, existingBranch);
                unit.BranchRipository.Update(existingBranch);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteBranch(int id)
        {
            try
            {
                var existingBranch = unit.BranchRipository.GetById(id);
                if (existingBranch == null)
                {
                    return false; 
                }

                existingBranch.IsDeleted = true;
                unit.BranchRipository.Update(existingBranch);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }


    }
}
