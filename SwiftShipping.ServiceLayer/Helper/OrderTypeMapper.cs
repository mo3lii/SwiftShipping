using SwiftShipping.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Helper
{
    public static class OrderTypeMapper
    {
        public static Dictionary<OrderType, string> OrderTypeDictionary = new Dictionary<OrderType, string>()
        {
            {OrderType.Normal,"Normal Shipping" },
            {OrderType.PickUp,"Pickup Shipping" },

        };
    }
}
