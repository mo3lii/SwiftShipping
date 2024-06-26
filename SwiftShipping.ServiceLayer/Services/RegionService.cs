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
        public RegionService(UnitOfWork _unit)
        {
            unit = _unit;
        }

        public bool AddRegion(RegionDTO regionDTO)
        {
            try
            {
                var region = new Region() { 
                    Name = regionDTO.name, 
                    GovernmentId = regionDTO.governmentId, 
                    NormalPrice = regionDTO.normalPrice, 
                    PickupPrice = regionDTO.pickupPrice 
                };
                unit.RegionRipository.Insert(region);
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
