using AutoMapper;
using E_Commerce.API.Dtos;
using E_Commerce.Domain.Entities;
using System.Runtime.InteropServices;

namespace E_Commerce.API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(b => b.ProductBrand, t => t.MapFrom(s => s.ProductBrand.Name))
                .ForMember(t => t.ProductType, O => O.MapFrom(t => t.ProductType.Name))
                .ForMember(t => t.PictureUrl, O => O.MapFrom<ProductURLResolver>());

            CreateMap<Brand, BrandToReturnDto>();

            CreateMap<Types,TypeToReturnDto>();
        }
    }
}
