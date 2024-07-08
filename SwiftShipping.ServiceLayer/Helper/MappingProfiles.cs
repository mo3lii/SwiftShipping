using AutoMapper;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {


            CreateMap<Order, OrderGetDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StatusMapper.StatusDictionary[src.Status]))
                .ForMember(dest => dest.ShippingType, opt => opt.MapFrom(src => ShippingTypeMapper.ShippingTypeDictionary[src.ShippingType]))
                .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => OrderTypeMapper.OrderTypeDictionary[src.OrderType]))
                .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => PaymentTypeMapper.PaymentTypeDictionary[src.PaymentType]))
                .ForMember(dest => dest.Government, opt => opt.MapFrom(src => src.Region.Government.Name))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region.Name))
                .ForMember(dest => dest.DeliveryCost, opt => opt.MapFrom(src => src.DeliveryCost))
                .ForMember(dest => dest.OrderStatus, opt =>opt.MapFrom(src => src.Status));


            CreateMap<OrderDTO, Order>();

            CreateMap<BranchDTO,Branch>();
            CreateMap<Branch, BranchGetDTO>()
                .ForMember(dest => dest.GovernmentName, opt => opt.MapFrom(src => src.Government.Name));

            CreateMap<Government, GovernmentGetDTO>();
            
            CreateMap<RegionDTO,Region>().ReverseMap();

            CreateMap<Region, RegionGetDTO>()
                .ForMember(dest => dest.GovernmentName, opt => opt.MapFrom(src => src.Government.Name));


            CreateMap<DeliveryManDTO, ApplicationUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.password));
            
            CreateMap<EmployeeDTO, ApplicationUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.password));

            CreateMap<SellerDTO, ApplicationUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.password));

            CreateMap<DeliveryMan, DeliveryManDTO>()
              .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.User.PasswordHash))
              .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.User.Email))
              .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.User.Address))
              .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.User.UserName))
              .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
              .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.User.Name)).ReverseMap();

            CreateMap<DeliveryMan, DeliveryManGetDTO>()
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<Seller, SellerGetDTO>()
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
              .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
              .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
              .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch.Id))
              .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name))
              .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.Region.Id));
                


            CreateMap<ApplicationUser, DeliveryManDTO>().ForMember(dest => dest.password, opt => opt.MapFrom(src => src.PasswordHash)); ;


            CreateMap<EmployeeDTO, Employee>();
            CreateMap< EmployeeGetDTO, Employee>(); 
            CreateMap<ApplicationUser, EmployeeDTO>();
            CreateMap<EmployeeDTO, ApplicationUser>();

            CreateMap<Employee,EmployeeDTO >()
                .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.User.PasswordHash))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.User.Name));


                    CreateMap<Employee, EmployeeGetDTO>()
            .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.User.Address))
            .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.User.PasswordHash))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.branchName, opt => opt.MapFrom(src => src.Branch.Name));
    

            CreateMap<ApplicationUser, EmployeeDTO>().ForMember(dest => dest.password, opt => opt.MapFrom(src => src.PasswordHash));

            CreateMap<Seller, SellerDTO>().ReverseMap();
            CreateMap<SellerGetDTO, Seller>();

            CreateMap<ApplicationUser, SellerDTO>();

            CreateMap<RolePermissions, PermissionDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => DepartmentMapper.DepartmentsDictionary[src.DepartmentId])).ReverseMap();

            CreateMap<GovernmentDTO, Government>().ReverseMap();

            CreateMap<DeliveryManRegions, RegionGetDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Region.Id))
                     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Region.Name))
                     .ForMember(dest => dest.PickupPrice, opt => opt.MapFrom(src => src.Region.PickupPrice))
                     .ForMember(dest => dest.NormalPrice, opt => opt.MapFrom(src => src.Region.NormalPrice))
                     .ForMember(dest => dest.GovernmentName, opt => opt.MapFrom(src => src.Region.Government.Name));




        }
    }
}
