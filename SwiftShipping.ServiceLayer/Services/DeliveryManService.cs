﻿using AutoMapper;
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
    public class DeliveryManService
    {
        private UnitOfWork unit;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper _mapper;

        public DeliveryManService(UnitOfWork _unit, UserManager<ApplicationUser> _userManager,
           RoleManager<IdentityRole> _roleManager,
           IMapper mapper)
        {
            unit = _unit;
            userManager = _userManager;
            roleManager = _roleManager;
            _mapper = mapper;

        }

        public async Task<bool> AddDliveryManAsync(DeliveryManDTO deliveryManDTO)
        {
            var appUser = _mapper.Map<DeliveryManDTO, ApplicationUser>(deliveryManDTO);
            IdentityResult result = await userManager.CreateAsync(appUser, deliveryManDTO.password);
            if (result.Succeeded)
            {
                //Delivery Man Role as string
                var DeliveryManRole = RoleTypes.DeliveryMan.ToString();

                // check if the role is exist if not, add it
                if (await roleManager.FindByNameAsync(DeliveryManRole) == null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = DeliveryManRole });
                // assign roles  to created user
                IdentityResult deliveryManRole = await userManager.AddToRoleAsync(appUser, DeliveryManRole);
                if (deliveryManRole.Succeeded)
                {
                    var deliveryMan = _mapper.Map<DeliveryManDTO, DeliveryMan>(deliveryManDTO);
                    deliveryMan.UserId = appUser.Id;
                    unit.DeliveryManRipository.Insert(deliveryMan);
                    unit.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool assignDeliveryManTORegion(int DeliveyManId, int RegionId)
        {
            try
            {
                var deliveryManRegion = new DeliveryManRegions()
                {
                    DeliveryManId = DeliveyManId,
                    RegionId = RegionId
                };
                unit.DeliveryManRegionsRipository.Insert(deliveryManRegion);
                unit.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<OrderGetDTO> getDeliveryManOrders(int deliveryManId)
        {
            var orders =  unit.OrderRipository.GetAll(o => o.DeliveryId==deliveryManId);
            return _mapper.Map<List<Order>, List<OrderGetDTO>>(orders);
        }

        public DeliveryManGetDTO GetById(int id)
        {
            var deliveryMan = unit.DeliveryManRipository.GetById(id);
            return _mapper.Map<DeliveryMan, DeliveryManGetDTO>(deliveryMan);
        }

        public List<DeliveryManGetDTO> GetAll()
        {
            var deliveryMenData = unit.DeliveryManRipository.GetAll();
            return _mapper.Map<List<DeliveryMan>, List<DeliveryManGetDTO>>(deliveryMenData);
        }

        public bool UpdateDeliveryMan(int id, DeliveryManDTO deliveryManDTO)
        {
            try
            {
                var existingDeliveryMan = unit.DeliveryManRipository.GetById(id);
                if (existingDeliveryMan == null)
                {
                    return false; 
                }
                var DeliveryManUser = unit.AppUserRepository.GetById(existingDeliveryMan.UserId);
                _mapper.Map(deliveryManDTO, existingDeliveryMan);
                _mapper.Map(existingDeliveryMan, DeliveryManUser);
                unit.DeliveryManRipository.Update(existingDeliveryMan);
                unit.AppUserRepository.Update(DeliveryManUser);

                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteDeliveryMan(int id)
        {
            try
            {
                var existingDeliveryMan = unit.DeliveryManRipository.GetById(id);
                if (existingDeliveryMan == null)
                {
                    return false;
                }
                var existingUser = unit.AppUserRepository.GetById(existingDeliveryMan.UserId);

                existingDeliveryMan.IsDeleted = true;
                existingUser.IsDeleted = true;

                unit.DeliveryManRipository.Update(existingDeliveryMan);
                unit.AppUserRepository.Update(existingUser);
                unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool AssignRegionsToDeliveryMan(int deliveryManId, int[]regions)
        {
            try
            {
                var deliveryMan = unit.DeliveryManRipository.GetById(deliveryManId);
                if (deliveryMan == null)
                {
                    return false;
                }
                
                for(int i = 0; i <regions.Length; i++)
                {
                    unit.DeliveryManRegionsRipository.Insert(new DeliveryManRegions()
                    {
                        DeliveryManId = deliveryMan.Id,
                        RegionId = regions[i]
                    });
                }
                unit.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    
        public List<DeliveryManRegions> GetDeliveryManRegions(int deliveryManId) {

            var deliveryMan = unit.DeliveryManRipository.GetById(deliveryManId);
            return deliveryMan.DeliveryManRegions;
        }
    }
}
