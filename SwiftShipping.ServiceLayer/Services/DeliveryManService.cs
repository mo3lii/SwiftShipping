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
    public class DeliveryManService
    {
        private UnitOfWork _unit;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public DeliveryManService(UnitOfWork unit, UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager,
           IMapper mapper)
        {
            _unit = unit;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _signInManager = signInManager;


        }

        public async Task<(bool Success, string UserId, string Role)> Login(LoginDTO loginDTO)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(loginDTO.email);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginDTO.userName);
            }

            if (user != null)
            {
                bool found = await _userManager.CheckPasswordAsync(user, loginDTO.password);
                if (found)
                {
                    await _signInManager.SignInAsync(user, loginDTO.RemembreMe);
                    // Fetch user roles
                    var roles = await _userManager.GetRolesAsync(user);
                    string role = roles.FirstOrDefault();

                    return (true, user.Id, role);
                }
            }

            return (false, null, null);
        }

        public async Task<(bool isSuccess, int deliveryManId)> AddDliveryManAsync(DeliveryManDTO deliveryManDTO)
        {
            var appUser = _mapper.Map<DeliveryManDTO, ApplicationUser>(deliveryManDTO);
            IdentityResult result = await _userManager.CreateAsync(appUser, deliveryManDTO.password);
            if (result.Succeeded)
            {
                // Delivery Man Role as string
                var DeliveryManRole = RoleTypes.DeliveryMan.ToString();

                // Check if the role exists, if not, add it
                if (await _roleManager.FindByNameAsync(DeliveryManRole) == null)
                    await _roleManager.CreateAsync(new IdentityRole() { Name = DeliveryManRole });

                // Assign roles to the created user
                IdentityResult deliveryManRole = await _userManager.AddToRoleAsync(appUser, DeliveryManRole);
                if (deliveryManRole.Succeeded)
                {
                    var deliveryMan = _mapper.Map<DeliveryManDTO, DeliveryMan>(deliveryManDTO);
                    deliveryMan.UserId = appUser.Id;
                    _unit.DeliveryManRipository.Insert(deliveryMan);
                    _unit.SaveChanges();

                    // Get the ID of the newly added delivery man
                    int deliveryManId = deliveryMan.Id;

                    return (true, deliveryManId);
                }
            }
            return (false, 0);
        }

        //public async Task<bool> AddDliveryManAsync(DeliveryManDTO deliveryManDTO)
        //{
        //    var appUser = _mapper.Map<DeliveryManDTO, ApplicationUser>(deliveryManDTO);
        //    IdentityResult result = await _userManager.CreateAsync(appUser, deliveryManDTO.password);
        //    if (result.Succeeded)
        //    {
        //        //Delivery Man Role as string
        //        var DeliveryManRole = RoleTypes.DeliveryMan.ToString();

        //        // check if the role is exist if not, add it
        //        if (await _roleManager.FindByNameAsync(DeliveryManRole) == null)
        //            await _roleManager.CreateAsync(new IdentityRole() { Name = DeliveryManRole });
        //        // assign roles  to created user
        //        IdentityResult deliveryManRole = await _userManager.AddToRoleAsync(appUser, DeliveryManRole);
        //        if (deliveryManRole.Succeeded)
        //        {
        //            var deliveryMan = _mapper.Map<DeliveryManDTO, DeliveryMan>(deliveryManDTO);
        //            deliveryMan.UserId = appUser.Id;
        //            _unit.DeliveryManRipository.Insert(deliveryMan);
        //            _unit.SaveChanges();
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public bool assignDeliveryManTORegion(int DeliveyManId, int RegionId)
        //{
        //    try
        //    {
        //        var deliveryManRegion = new DeliveryManRegions()
        //        {
        //            DeliveryManId = DeliveyManId,
        //            RegionId = RegionId
        //        };
        //        _unit.DeliveryManRegionsRipository.Insert(deliveryManRegion);
        //        _unit.SaveChanges();
        //        return true;
        //    }
        //    catch { return false; }
        //}

        public List<OrderGetDTO> GetDeliveryManOrders(int deliveryManId , OrderStatus? status = null)
        {
           
            if (status != null) {
                var ordersByStatus =  _unit.OrderRipository.GetAll(order => order.DeliveryId == deliveryManId 
                && order.Status == status && order.IsDeleted == false);

                return _mapper.Map<List<Order>, List<OrderGetDTO>>(ordersByStatus);
            }

            var orders =  _unit.OrderRipository.GetAll(order => order.DeliveryId == deliveryManId && order.IsDeleted == false);

            return _mapper.Map<List<Order>, List<OrderGetDTO>>(orders);
        }

        public DeliveryManGetDTO GetById(int id)
        {
            var deliveryMan = _unit.DeliveryManRipository.GetById(id);
            return _mapper.Map<DeliveryMan, DeliveryManGetDTO>(deliveryMan);
        }

        public List<DeliveryManGetDTO> GetAll()
        {
            var deliveryMenData = _unit.DeliveryManRipository.GetAll(deliveryMan => deliveryMan.IsDeleted == false);

            return _mapper.Map<List<DeliveryMan>, List<DeliveryManGetDTO>>(deliveryMenData);
        }

        public bool UpdateDeliveryMan(int id, DeliveryManDTO deliveryManDTO)
        {
            try
            {
                var existingDeliveryMan = _unit.DeliveryManRipository.GetById(id);
                if (existingDeliveryMan == null)
                {
                    return false; 
                }

                var DeliveryManUser = _unit.AppUserRepository.GetById(existingDeliveryMan.UserId);

                _mapper.Map(deliveryManDTO, existingDeliveryMan);

                _mapper.Map(existingDeliveryMan, DeliveryManUser);

                _unit.DeliveryManRipository.Update(existingDeliveryMan);

                DeliveryManUser.NormalizedUserName = _userManager.NormalizeName(deliveryManDTO.userName);
                DeliveryManUser.NormalizedEmail = _userManager.NormalizeEmail(deliveryManDTO.email);

                _unit.AppUserRepository.Update(DeliveryManUser);

                _unit.SaveChanges();
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
                var existingDeliveryMan = _unit.DeliveryManRipository.GetById(id);
                if (existingDeliveryMan == null)
                {
                    return false;
                }
                var existingUser = _unit.AppUserRepository.GetById(existingDeliveryMan.UserId);

                existingDeliveryMan.IsDeleted = true;
                existingUser.IsDeleted = true;

                _unit.DeliveryManRipository.Update(existingDeliveryMan);
                _unit.AppUserRepository.Update(existingUser);
                _unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
     public bool AssignRegionsToDeliveryMan(int deliveryManId, int[] regionIds)
        {
            try
            {
                foreach (var regionId in regionIds)
                {
                    var deliveryManRegion = new DeliveryManRegions
                    {
                        DeliveryManId = deliveryManId,
                        RegionId = regionId
                    };
                    _unit.DeliveryManRegionsRipository.Insert(deliveryManRegion);
                }
                _unit.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<DeliveryManRegions> GetDeliveryManRegions(int deliveryManId) {

            var deliveryMan = _unit.DeliveryManRipository.GetById(deliveryManId);
            return deliveryMan.DeliveryManRegions;
        }
    }
}
