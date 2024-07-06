using Microsoft.AspNetCore.Identity;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    public class WeightSettingService
    {
        private UnitOfWork unit;
        public WeightSettingService(UnitOfWork _unit)
        {
            unit = _unit;
        }
        public async Task<bool> UpdateSetting(WeightSetting weightSetting )
        {
            try
            {
                unit.WeightSettingRepository.UpdateSetting(weightSetting);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<WeightSetting> GetSettingAsync()
        {
            return unit.WeightSettingRepository.GetSetting();
        }
    }
}
