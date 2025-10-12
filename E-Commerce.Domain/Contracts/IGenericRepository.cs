using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<T> where T : BaseClass
    {
        public Task<T?> GetAsync(ISpecifications<T> spc);
        public Task<IEnumerable<T?>> GetAllAsync(ISpecifications<T> spc);
        public Task<IEnumerable<T?>> GetAllAsyncWithFilter(ISpecifications<T> spc);
        public Task Add(T element);
        public Task Update(T element);
        public Task Delete(T element);
       
    }
}
