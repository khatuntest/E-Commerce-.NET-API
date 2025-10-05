using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<T> where T : BaseClass
    {
        public Task<T> GetAsync(int Id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task Add(T element);
        public Task Update(T element);
        public Task Delete(T element);
       
    }
}
