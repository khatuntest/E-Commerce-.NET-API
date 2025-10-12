using AutoMapper;
using E_Commerce.API.Dtos;
using E_Commerce.Domain.Entities;

namespace E_Commerce.API.Profiles
{
    public class ProductURLResolver (IConfiguration configuration): IValueResolver<Product, ProductToReturnDto, string>
    {
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }

            var BaseURL = configuration["APIBaseURL"];
           
            return  BaseURL+ source.PictureUrl;
            
        }
    }
}
