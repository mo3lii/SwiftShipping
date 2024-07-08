using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class GovernmentService
    {
        private readonly UnitOfWork _unit;
        private readonly IMapper _mapper;
        public GovernmentService(UnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            this._mapper = mapper;
        }

        public bool AddGovernment(string name)
        {
            try { 
                _unit.GovernmentRipository.Insert(new Government() { Name = name, IsActive = true });
                _unit.SaveChanges();
            }catch { 
            return false;
            }
            return true;
        }
        public List<GovernmentGetDTO> GetAll()
        {
            var governments = _unit.GovernmentRipository.GetAll(government => government.IsDeleted == false);

            return _mapper.Map<List<Government>, List<GovernmentGetDTO>>(governments);
        }

        public GovernmentGetDTO GetById(int id)
        {
            var government = _unit.GovernmentRipository.GetById(id);
            return _mapper.Map<Government, GovernmentGetDTO>(government);

        }

        public bool EditGovernment(int id, GovernmentDTO governmentDTO)
        {
            try
            {
                var foundgovernment = _unit.GovernmentRipository.GetById(id);
                if (foundgovernment == null)
                {
                    return false;
                }

                _mapper.Map(governmentDTO, foundgovernment);
                _unit.GovernmentRipository.Update(foundgovernment);
                _unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var foundGovernment = _unit.GovernmentRipository.GetById(id);
                if (foundGovernment == null)
                {
                    return false;
                }

                foundGovernment.IsDeleted = true;
                _unit.GovernmentRipository.Update(foundGovernment);
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
