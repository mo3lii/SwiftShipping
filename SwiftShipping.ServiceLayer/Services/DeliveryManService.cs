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
    public class DeliveryManService
    {
        private UnitOfWork unit;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DeliveryManService(UnitOfWork _unit, UserManager<ApplicationUser> _userManager,
           RoleManager<IdentityRole> _roleManager)
        {
            unit = _unit;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public async Task<bool> AddDliveryManAsync(DeliveryManDTO deliveryManDTO)
        {
            ApplicationUser appUser = new ApplicationUser()
            {
                UserName = deliveryManDTO.userName,
                Email = deliveryManDTO.email,
                PasswordHash = deliveryManDTO.password,
                Address = deliveryManDTO.address,
                PhoneNumber = deliveryManDTO.phoneNumber,
                Name = deliveryManDTO.name,
            };

            IdentityResult result = await userManager.CreateAsync(appUser, deliveryManDTO.password);
            if (result.Succeeded)
            {

                // check if the role is exist if not, add it
                if (await roleManager.FindByNameAsync("deliveryman") == null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "deliveryman" });

                // assign roles  to created user
                IdentityResult deliveryManRole = await userManager.AddToRoleAsync(appUser, "deliveryman");


                if (deliveryManRole.Succeeded)
                {

                    DeliveryMan DeliveryMan = new DeliveryMan()
                    {
                        UserId = appUser.Id,
                        Name= deliveryManDTO.name,
                    };
                    unit.DeliveryManRipository.Insert(DeliveryMan);
                    unit.SaveChanges();
                    return true;
                }

            }

            return false;
        }
    }
}
