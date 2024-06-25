using SwiftShipping.API.DTO;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    internal class SellerService
    {
        private UnitOfWork unit;
        public SellerService(UnitOfWork _unit)
        {
            unit = _unit;
        }
        public bool addSeller(SellerDTO sellerDTO)
        {
            //ApplicationUser user =  new ApplicationUser()
            //{
            //    Email = sellerDTO.email,
            //    PasswordHash = sellerDTO.password,

            //}
            
            return false;
        }
    }
}
