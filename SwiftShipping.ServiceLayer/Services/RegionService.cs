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
        private UnitOfWork unit;
        private readonly IMapper mapper;
        public RegionService(UnitOfWork _unit, IMapper mapper)
        {
            unit = _unit;
            this.mapper = mapper;
        }

        public bool Add(RegionDTO regionDTO)
        {
            try
            {
                var region = mapper.Map<RegionDTO, Region>(regionDTO);
                unit.RegionRipository.Insert(region);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<RegionGetDTO> GetAll()
        {
            var regions = unit.RegionRipository.GetAll();
            return mapper.Map<List<Region>, List<RegionGetDTO>>(regions);
        }

        public RegionDTO GetById(int id)
        {
            var region = unit.RegionRipository.GetById(id);
            return mapper.Map<Region, RegionDTO>(region);

        }

    }
}
