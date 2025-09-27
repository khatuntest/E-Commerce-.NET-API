using AutoMapper;
using E_Commerce.API.Dtos;
using E_Commerce.Domain.Entities;
using System.Runtime.InteropServices;

namespace E_Commerce.API
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(b => b.ProductBrand, t => t.MapFrom(s => s.ProductBrand.Name))
                .ForMember(t => t.ProductType , O => O.MapFrom(t => t.ProductType.Name));

            CreateMap<Brand, BrandToReturnDto>();

            CreateMap<Types,TypeToReturnDto>();
        }
    }
}
