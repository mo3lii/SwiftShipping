using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Enum
{
    public enum OrderStatus
    {
        New = 1,
        AcceptedByDeliveryCompany,
        RejectedByDeliveryCompany,
        Pending,
        DeliveredToDeliveryMan,
        CanNotBeReached,
        Postponed,
        PartiallyDelivered,
        CanceledByCustomer,
        RejectWithPayment,
        RejectWithoutPayment,
        RejectWithPartiallyPaid
    }
}
