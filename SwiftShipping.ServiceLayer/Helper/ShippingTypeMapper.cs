using SwiftShipping.DataAccessLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Helper
{
    public static class ShippingTypeMapper
    {
        public static Dictionary<ShippingType, string> ShippingTypeDictionary = new Dictionary<ShippingType, string>()
        {
            {ShippingType.SameDay,"Same Day" },
            {ShippingType.In24H,"24 Hours" },
            {ShippingType.In2to5Days,"2 to 5 Days" },
            
        };
    }
}
