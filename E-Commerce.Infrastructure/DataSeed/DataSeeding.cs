using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.DataSeed
{
    public static class DataSeeding
    {
        public static async void AddData(ECommerceContext commerceContext)
        {
            if(!commerceContext.Brands.Any())
            {
                var BransData  = File.ReadAllTextAsync("../E-Commerce.Infrastructure/DataJSON/Brands.json");
                var BransList = JsonSerializer.Deserialize<List<Brand>>(await BransData);
                if(BransList?.Any() == true)
                {
                    await commerceContext.Brands.AddRangeAsync(BransList);
                    await commerceContext.SaveChangesAsync();
                   
                }

            }


            if (!commerceContext.Types.Any())
            {
                var TypesData = await File.ReadAllTextAsync("../E-Commerce.Infrastructure/DataJSON/Types.json");
                var TypesList = JsonSerializer.Deserialize<List<Types>>(TypesData);
                if(TypesList?.Any() == true)
                {
                    await commerceContext.Types.AddRangeAsync(TypesList);
                    await commerceContext.SaveChangesAsync();
                    
                }
            }


            if (!commerceContext.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../E-Commerce.Infrastructure/DataJSON/Products.json");
                var productList = JsonSerializer.Deserialize<List<Product>>(productsData);
                if(productList?.Any() == true)
                {
                    await commerceContext.Products.AddRangeAsync(productList);
                    await commerceContext.SaveChangesAsync();
                    
                }
            }
        }
    }
}
