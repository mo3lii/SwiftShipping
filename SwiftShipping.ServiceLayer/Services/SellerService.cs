using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SwiftShipping.DataAccessLayer.Enum;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using SwiftShipping.ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    public class SellerService
    {
        private UnitOfWork unit;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SellerService(
            UnitOfWork _unit, 
            UserManager<ApplicationUser> _userManager, 
            RoleManager<IdentityRole> _roleManager,
            IMapper _mapper)
        {
            unit = _unit;
            mapper = _mapper;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        public async Task<bool> addSellerAsync(SellerDTO sellerDTO)
        {
            var appUser = mapper.Map<SellerDTO, ApplicationUser>(sellerDTO);

            IdentityResult result = await userManager.CreateAsync(appUser, sellerDTO.password);
            if (result.Succeeded)
            {
                //Seller Role as string
                var SellerRole = RoleTypes.Seller.ToString();

                // check if the role is exist if not, add it
                if (await roleManager.FindByNameAsync(SellerRole) == null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = SellerRole });

                // assign roles  to created user
                IdentityResult sellerRole = await userManager.AddToRoleAsync(appUser, SellerRole);
            

                if (sellerRole.Succeeded ) {

                    
                    var seller = mapper.Map<SellerDTO, Seller>(sellerDTO);
                    seller.UserId = appUser.Id;
                    unit.SellerRipository.Insert(seller);
                    unit.SaveChanges();
                    return true;
                }
              
            }
         
            return false;
        }

        public SellerGetDTO GetById(int id)
        {
            var seller = unit.SellerRipository.GetById(id);
            return mapper.Map<Seller, SellerGetDTO>(seller);

        }

        public List<SellerGetDTO> GetAll()
        {
            var sellers = unit.SellerRipository.GetAll();
            return mapper.Map<List<Seller>, List<SellerGetDTO>>(sellers);
        }

        public List<OrderGetDTO> GetSellerOrders(int id)
        {
            var seller = unit.SellerRipository.GetById(id);
            var sellerOrders = seller?.Orders;
            return mapper.Map<List<Order>, List<OrderGetDTO>>(sellerOrders);
        }

        public bool Update(int id, SellerDTO sellerDTO)
        {
            try
            {
                var foundSeller = unit.SellerRipository.GetById(id);
                //app user
                if (foundSeller == null)
                {
                    return false;
                }
                var existingSellerUser = unit.AppUserRepository.GetById(foundSeller.UserId);

                mapper.Map(sellerDTO, foundSeller);
                mapper.Map(sellerDTO, existingSellerUser);
                unit.SellerRipository.Update(foundSeller);
                unit.AppUserRepository.Update(existingSellerUser);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var foundSeller = unit.SellerRipository.GetById(id);
                var existingSellerUser = unit.AppUserRepository.GetById(foundSeller.UserId);
                if (foundSeller == null)
                {
                    return false;
                }
                foundSeller.IsDeleted = true;
                existingSellerUser.IsDeleted = true;

                unit.SellerRipository.Update(foundSeller);
                unit.AppUserRepository.Update(existingSellerUser);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }



    }
}
