using SwiftShipping.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Repository.CustomRepository
{
    public class WeightSettingRepository
    {
        private ApplicationContext _context;
        public WeightSettingRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void UpdateSetting(WeightSetting weightSettings)
        {
            var setting  = _context.WeightSettings.FirstOrDefault();
            if (setting != null)
            {
                setting.DefaultWeight = weightSettings.DefaultWeight;
                setting.KGPrice = weightSettings.KGPrice;
                _context.WeightSettings.Update(setting);
                _context.SaveChanges();
            }
        }
        public WeightSetting GetSetting()
        {
            return _context.WeightSettings.FirstOrDefault();
        }
    }
}
