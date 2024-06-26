﻿namespace SwiftShipping.ServiceLayer.DTO
{
    public class SellerDTO
    {
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }

        public string userName { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string storeName { get; set; }
        public int regionId { get; set; }

        public int branchId { get; set; }
    }
}
