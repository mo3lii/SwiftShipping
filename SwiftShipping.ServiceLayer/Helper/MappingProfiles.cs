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
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region.Name));

            CreateMap<OrderDTO, Order>();

            CreateMap<BranchDTO,Branch>();
            CreateMap<Branch, BranchGetDTO>()
                .ForMember(dest => dest.GovernmentName, opt => opt.MapFrom(src => src.Government.Name));

            CreateMap<Government, GovernmentGetDTO>();
            
            CreateMap<Region,RegionDTO>();

            CreateMap<Region,RegionGetDTO>()
                .ForMember(dest=>dest.GovernmentName,opt=>opt.MapFrom(src=>src.Government.Name));


            CreateMap<DeliveryManDTO, ApplicationUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.password));
            
            CreateMap<EmployeeDTO, ApplicationUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.password));

            CreateMap<SellerDTO, ApplicationUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.password));

            CreateMap<DeliveryManDTO, DeliveryMan>();
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<SellerDTO, Seller>();
        }
    }
}
