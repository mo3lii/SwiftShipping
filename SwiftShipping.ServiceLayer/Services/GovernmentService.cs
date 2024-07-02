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
        private UnitOfWork unit;
        private readonly IMapper mapper;
        public GovernmentService(UnitOfWork _unit, IMapper mapper)
        {
            unit = _unit;
            this.mapper = mapper;
        }

        public bool AddGovernment(string name)
        {
            try { 
            unit.GovernmentRipository.Insert(new Government() { Name = name, IsActive = true });
            unit.SaveChanges();
            }catch { 
            return false;
            }
            return true;
        }
        public List<GovernmentGetDTO> GetAll()
        {
            var governments = unit.GovernmentRipository.GetAll();
            return mapper.Map<List<Government>, List<GovernmentGetDTO>>(governments);
        }

        public GovernmentGetDTO GetById(int id)
        {
            var government = unit.GovernmentRipository.GetById(id);
            return mapper.Map<Government, GovernmentGetDTO>(government);

        }

        public bool EditGovernment(int id, GovernmentDTO governmentDTO)
        {
            try
            {
                var foundgovernment = unit.GovernmentRipository.GetById(id);
                if (foundgovernment == null)
                {
                    return false;
                }

                mapper.Map(governmentDTO, foundgovernment);
                unit.GovernmentRipository.Update(foundgovernment);
                unit.SaveChanges();
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
                var foundGovernment = unit.GovernmentRipository.GetById(id);
                if (foundGovernment == null)
                {
                    return false;
                }

                foundGovernment.IsDeleted = true;
                unit.GovernmentRipository.Update(foundGovernment);
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
