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
        private readonly UnitOfWork _unit;
        public WeightSettingService(UnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<bool> UpdateSetting(WeightSetting weightSetting )
        {
            try
            {
                _unit.WeightSettingRepository.UpdateSetting(weightSetting);
                _unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<WeightSetting> GetSettingAsync()
        {
            return _unit.WeightSettingRepository.GetSetting();
        }
    }
}
