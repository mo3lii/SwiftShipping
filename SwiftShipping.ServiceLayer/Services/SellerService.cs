using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
       
                // check if the role is exist if not, add it
                if (await roleManager.FindByNameAsync("seller") == null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "seller" });

                // assign roles  to created user
                IdentityResult sellerRole = await userManager.AddToRoleAsync(appUser, "seller");
            

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
    }
}
