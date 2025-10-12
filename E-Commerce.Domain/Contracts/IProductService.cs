
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IProductService
    {
        public Task ThrowTestException();//for testing


        public  Task<Product> CreateProduct();
        public Task<IEnumerable<Product>> GetAllWithFilter(ProductQueryParametersDtos parameters);
    }
}
