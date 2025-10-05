using E_Commerce.API.Dtos;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Interfaces
{
    public interface IProductService
    {
        public Task ThrowTestException();//for testing


        public  Task<(IReadOnlyList<Product> Products, int TotalCount)> GetProductsAsync(ProductQueryParameters parameters);
    }
}
