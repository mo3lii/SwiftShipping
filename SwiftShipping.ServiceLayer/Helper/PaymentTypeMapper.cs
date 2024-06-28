using SwiftShipping.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Helper
{
    public static class PaymentTypeMapper
    {
        public static Dictionary<PaymentType, string> PaymentTypeDictionary = new Dictionary<PaymentType, string>()
        {
            {PaymentType.MustBePaid,"Must Be Paid" },
            {PaymentType.Prepaid,"Prepaid" },
            {PaymentType.CargoForCargo,"Cargo For Cargo" },
        };
    }
}
