
using System;
using AutoMapper;
using SimpleSales.Api.Dtos.Order;
using SimpleSales.Api.Models;

namespace SimpleSales.Api.MappingProfiles.Order
{
    public class OrderModelMappingProfile : Profile
    {
        public OrderModelMappingProfile()
        {
            _ = CreateMap<CreateOrderDto, OrderModel>()
                .ForMember(d => d.Id, o => o.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(d => d.ProductId, o => o.MapFrom(src => src.ProductId))
                .ForMember(d => d.Quantity, o => o.MapFrom(src => src.Quantity))
                .ForMember(d => d.Date, o => o.MapFrom(src => DateTime.Now))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(src => "API"))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(src => DateTime.Now))
                ;
        }
    }
}
