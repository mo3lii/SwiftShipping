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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SellerService(UnitOfWork _unit, UserManager<ApplicationUser> _userManager, 
            RoleManager<IdentityRole> _roleManager)
        {
            unit = _unit;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        public async Task<bool> addSellerAsync(SellerDTO sellerDTO)
        {
            ApplicationUser appUser = new ApplicationUser()
            {
                UserName = sellerDTO.email,
                Email = sellerDTO.email,
                PasswordHash = sellerDTO.password,
                Address = sellerDTO.address,
                PhoneNumber = sellerDTO.phoneNumber,
                Name = sellerDTO.name,
            };

            IdentityResult result = await userManager.CreateAsync(appUser, sellerDTO.password);
            if (result.Succeeded)
            {
                // if role User not exist create it
                if (await roleManager.FindByNameAsync("Display") == null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "Display" });

                if (await roleManager.FindByNameAsync("Edit") == null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "Edit" });

                // assign roles  to created user
                IdentityResult DisplayRole = await userManager.AddToRoleAsync(appUser, "Display");
                IdentityResult EditRole = await userManager.AddToRoleAsync(appUser, "Edit");

                if (DisplayRole.Succeeded && EditRole.Succeeded) {

                    Seller seller = new Seller()
                    {
                        userId = appUser.Id,
                        RegionId = sellerDTO.regionId,
                        StoreName = sellerDTO.storeName,
                    };
                    unit.SellerRipository.Insert(seller);
                    unit.SaveChanges();
                    return true;
                }
              
            }
         
            return false;
        }
    }
}
