using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    public class BranchService
    {
        private UnitOfWork unit;
        public BranchService(UnitOfWork _unit)
        {
            unit = _unit;
        }

        public bool AddBrnach(BranchDTO branchDTO)
        {
            try
            {
                var branch = new Branch()
                {
                    Name = branchDTO.name,
                    GovernmentId = branchDTO.governmentId,
                };
                unit.BranchRipository.Insert(branch);
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
