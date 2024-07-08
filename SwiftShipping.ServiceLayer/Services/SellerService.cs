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
        private readonly UnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SellerService(
            UnitOfWork unit, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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

        public async Task<bool> addSellerAsync(SellerDTO sellerDTO)
        {
            var appUser = _mapper.Map<SellerDTO, ApplicationUser>(sellerDTO);

            IdentityResult result = await _userManager.CreateAsync(appUser, sellerDTO.password);
            if (result.Succeeded)
            {
                //Seller Role as string
                var SellerRole = RoleTypes.Seller.ToString();

                // check if the role is exist if not, add it
                if (await _roleManager.FindByNameAsync(SellerRole) == null)
                    await _roleManager.CreateAsync(new IdentityRole() { Name = SellerRole });

                // assign roles  to created user
                IdentityResult sellerRole = await _userManager.AddToRoleAsync(appUser, SellerRole);

                if (sellerRole.Succeeded ) {
                    var seller = _mapper.Map<SellerDTO, Seller>(sellerDTO);
                    seller.UserId = appUser.Id;
                    _unit.SellerRipository.Insert(seller);
                    _unit.SaveChanges();
                    return true;
                } 
            }
         
            return false;
        }

        public SellerGetDTO GetById(int id)
        {
            var seller = _unit.SellerRipository.GetById(id);
            return _mapper.Map<Seller, SellerGetDTO>(seller);

        }

        public List<SellerGetDTO> GetAll()
        {
            var sellers = _unit.SellerRipository.GetAll(seller => seller.IsDeleted == false);
            return _mapper.Map<List<Seller>, List<SellerGetDTO>>(sellers);
        }

        public List<OrderGetDTO> GetSellerOrders(int id)
        {
            var seller = _unit.SellerRipository.GetById(id);
            var sellerOrders = seller?.Orders.Where(order => order.IsDeleted == false).ToList();
            return _mapper.Map<List<Order>, List<OrderGetDTO>>(sellerOrders);
        }

        public bool Update(int id, SellerDTO sellerDTO)
        {
            try
            {
                var foundSeller = _unit.SellerRipository.GetById(id);
                //app user
                if (foundSeller == null)
                {
                    return false;
                }
                var existingSellerUser = _unit.AppUserRepository.GetById(foundSeller.UserId);

                _mapper.Map(sellerDTO, foundSeller);
                _mapper.Map(sellerDTO, existingSellerUser);
                _unit.SellerRipository.Update(foundSeller);

                existingSellerUser.NormalizedUserName = _userManager.NormalizeName(sellerDTO.userName);
                existingSellerUser.NormalizedEmail = _userManager.NormalizeEmail(sellerDTO.email);

                _unit.AppUserRepository.Update(existingSellerUser);
                _unit.SaveChanges();
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
                var foundSeller = _unit.SellerRipository.GetById(id);
                var existingSellerUser = _unit.AppUserRepository.GetById(foundSeller.UserId);
                if (foundSeller == null)
                {
                    return false;
                }

                foundSeller.IsDeleted = true;
                existingSellerUser.IsDeleted = true;

                _unit.SellerRipository.Update(foundSeller);
                _unit.AppUserRepository.Update(existingSellerUser);
                _unit.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public List<OrderGetDTO> GetSellerOrdersByStatus(int id, OrderStatus status)
        {
            var seller = _unit.SellerRipository.GetById(id);
            var sellerOrders = seller?.Orders.Where(order => order.Status == status && order.IsDeleted == false).ToList();
            return _mapper.Map<List<Order>, List<OrderGetDTO>>(sellerOrders);
        }

    }
}
