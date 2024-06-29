using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class SellerGetDTO
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string RegionName { get; set; }
        public string StoreName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }


    }
}
