using AutoMapper;
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
    public class RegionService
    {
        private readonly UnitOfWork _unit;
        private readonly IMapper _mapper;

        public RegionService(UnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public bool Add(RegionDTO regionDTO)
        {
            try
            {
                var region = _mapper.Map<RegionDTO, Region>(regionDTO);
                _unit.RegionRipository.Insert(region);
                _unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<RegionGetDTO> GetAll()
        {
            var regions = _unit.RegionRipository.GetAll(region => region.IsDeleted == false);

            return _mapper.Map<List<Region>, List<RegionGetDTO>>(regions);
        }

        public RegionGetDTO GetById(int id)
        {
            var region = _unit.RegionRipository.GetById(id);

            return _mapper.Map<Region, RegionGetDTO>(region);
        }

        public bool EditRegion(int id, RegionDTO regionDTO)
        {
            try
            {
                var foundRegion= _unit.RegionRipository.GetById(id);
                if (foundRegion == null)
                {
                    return false;
                }

                _mapper.Map(regionDTO, foundRegion);
                _unit.RegionRipository.Update(foundRegion);
                _unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteRegion(int id)
        {
            try
            {
                var foundRegion = _unit.RegionRipository.GetById(id);
                if (foundRegion == null)
                {
                    return false;
                }
                foundRegion.IsDeleted = true;
                _unit.RegionRipository.Update(foundRegion);
                _unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    
        public List<Region> GetRegionsByGovernment(int governrmrntId)
        {
            var regions =   _unit.RegionRipository.GetAll(region => region.GovernmentId == governrmrntId && region.IsDeleted == false);
            return regions;
        }
    }
}
