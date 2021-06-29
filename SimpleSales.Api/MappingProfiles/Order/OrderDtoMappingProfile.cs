
using AutoMapper;
using SimpleSales.Api.Dtos.Order;
using SimpleSales.Api.Models;

namespace SimpleSales.Api.MappingProfiles.Order
{
    public class OrderDtoMappingProfile : Profile
    {
        public OrderDtoMappingProfile()
        {
            _ = CreateMap<OrderModel, OrderDto>()
                    .ForMember(d => d.Id, o => o.MapFrom(src => src.Id))
                    .ForMember(d => d.Date, o => o.MapFrom(src => src.Date))
                    .ForMember(d => d.Product, o => o.MapFrom(src => src.Product))
                    .ForMember(d => d.UnitPrice, o => o.MapFrom(src => src.UnitPrice))
                    .ForMember(d => d.Quantity, o => o.MapFrom(src => src.Quantity))
                ;
        }
    }
}
