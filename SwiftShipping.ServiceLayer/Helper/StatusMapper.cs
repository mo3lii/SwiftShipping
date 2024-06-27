using SwiftShipping.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Helper
{
    public static class StatusMapper
    {
        public static Dictionary<OrderStatus, string> StatusDictionary = new Dictionary<OrderStatus, string>()
        {
            {OrderStatus.New,"New" },
            {OrderStatus.AcceptedByDeliveryCompany,"Accepted to delivery" },
            {OrderStatus.RejectedByDeliveryCompany,"Rejected delivery" },
            {OrderStatus.Pending,"Pending" },
            {OrderStatus.DeliveredToDeliveryMan,"With Delivery Man" },
            {OrderStatus.CanNotBeReached,"Can't Be Reached" },
            {OrderStatus.Postponed,"Postponed" },
            {OrderStatus.PartiallyDelivered,"Partially Delivered" },
            {OrderStatus.CanceledByCustomer,"Canceled By Customer" },
            {OrderStatus.RejectWithPayment,"Rejected & Paid" },
            {OrderStatus.RejectWithPartiallyPaid,"Rejected & Partially Paid" },
            {OrderStatus.RejectWithoutPayment,"Rejected & Not Paid" },
        };
    }
}
