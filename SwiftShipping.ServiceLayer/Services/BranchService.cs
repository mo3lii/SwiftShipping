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
        private readonly UnitOfWork _unit;
        private readonly IMapper _mapper;

        public BranchService(UnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public bool AddBrnach(BranchDTO branchDTO)
        {
            try
            {
                var branch = _mapper.Map<BranchDTO,Branch>(branchDTO);
               branch.CreationDate = DateTime.Now;
                _unit.BranchRipository.Insert(branch);
                _unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    
        public IEnumerable<BranchGetDTO> GetAll()
        {
            var branches = _unit.BranchRipository.GetAll(branch => branch.IsDeleted == false);

            return _mapper.Map<IEnumerable<Branch>, IEnumerable<BranchGetDTO>>(branches);
        }

        public BranchGetDTO GetById(int id)
        {
            var branch = _unit.BranchRipository.GetById(id);
            return _mapper.Map<Branch, BranchGetDTO>(branch);

        }


        public bool EditBranch(int id, BranchDTO branchDTO)
        {
            try
            {
                var existingBranch = _unit.BranchRipository.GetById(id);

                if (existingBranch == null)
                {
                    return false;
                }

                _mapper.Map(branchDTO, existingBranch);
                _unit.BranchRipository.Update(existingBranch);
                _unit.SaveChanges();
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
                var existingBranch = _unit.BranchRipository.GetById(id);
                if (existingBranch == null)
                {
                    return false; 
                }

                existingBranch.IsDeleted = true;
                existingBranch.IsActive = false;
                _unit.BranchRipository.Update(existingBranch);
                _unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }


    }
}
