using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;
using E_Commerce.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseClass
    {
        private readonly ECommerceContext CommerceContext;

        public GenericRepository(ECommerceContext _commerceContext) 
        {
            CommerceContext = _commerceContext;
        }

        public async Task<T?> GetAsync(ISpecifications<T> spc)
         => await ApplySpecification(spc).FirstOrDefaultAsync();
        

        public async Task<IEnumerable<T?>> GetAllAsync(ISpecifications<T> spc)
            =>  await ApplySpecification(spc).ToListAsync();


        public async Task<IEnumerable<T?>> GetAllAsyncWithFilter(ISpecifications<T> spc)
            => await ApplySpecification(spc).ToListAsync();


        public async Task Add(T element)
        {
            await CommerceContext.AddAsync(element);
            await CommerceContext.SaveChangesAsync();

        }

        public async Task Update(T element)
        {
             CommerceContext.Update(element);
            await CommerceContext.SaveChangesAsync();
        }

        public async Task Delete(T element)
        {
            CommerceContext.Remove(element);
            await CommerceContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> spc)
            => SpecificationEvaluated<T>.GetQuery(CommerceContext.Set<T>(), spc);

   
    }
}
