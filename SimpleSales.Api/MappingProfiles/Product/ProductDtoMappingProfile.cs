
using AutoMapper;
using SimpleSales.Api.Dtos.Product;
using SimpleSales.Api.Models;

namespace SimpleSales.Api.MappingProfiles.Product
{
    public class ProductDtoMappingProfile : Profile
    {
        public ProductDtoMappingProfile()
        {
            _ = CreateMap<ProductModel, ProductDto>()
                .ForMember(d => d.Id, o => o.MapFrom(src => src.Id))
                .ForMember(d => d.Name, o => o.MapFrom(src => src.Name))
                .ForMember(d => d.UnitPrice, o => o.MapFrom(src => src.UnitPrice))
                .ForMember(d => d.Quantity, o => o.MapFrom(src => src.Quantity))
                ;
        }
    }
}
