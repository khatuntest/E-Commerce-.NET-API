using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
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
        private readonly ECommerceContext _CommerceContext;

        public GenericRepository(ECommerceContext commerceContext) 
        {
            _CommerceContext = commerceContext;
        }

        public async Task<T?> GetAsync(int Id)
        {
            if(typeof(T) == typeof(Product))
            return await  _CommerceContext.Set<Product>()
                    .Include(p => p.ProductBrand)
                    .Include(p => p.ProductType)
                    .Where(p =>  p.Id == Id).FirstOrDefaultAsync() as T;

            if (typeof(T) == typeof(Brand))
                return await _CommerceContext.Set<Brand>()
                        .Include(b => b.Products)
                        .ThenInclude(p => p.ProductType)
                        .Where(p => p.Id == Id).FirstOrDefaultAsync() as T;

            if (typeof(T) == typeof(Types))
                return await _CommerceContext.Set<Types>()
                    .Include(t => t.Products)
                    .ThenInclude(p => p.ProductBrand)
                    .Where(p => p.Id == Id).FirstOrDefaultAsync() as T;

            return await _CommerceContext.Set<T>().FindAsync(Id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IEnumerable<T>) await _CommerceContext
                    .Set<Product>()
                    .Include(p => p.ProductBrand)
                    .Include(p => p.ProductType)
                    .ToListAsync();

            if (typeof(T) == typeof(Brand))
                return (IEnumerable<T>)await _CommerceContext
                    .Set<Brand>()
                     .Include(b => b.Products)
                     .ThenInclude(p => p.ProductType)
                    .ToListAsync();

            if (typeof(T) == typeof(Types))
                return (IEnumerable<T>)await _CommerceContext
                    .Set<Types>()
                    .Include(t => t.Products)
                    .ThenInclude(p => p.ProductBrand)
                    .ToListAsync();

            return await _CommerceContext.Set<T>().ToListAsync();
        }
        public async Task Add(T element)
        {
            await _CommerceContext.AddAsync(element);
            await _CommerceContext.SaveChangesAsync();

        }

        public async Task Update(T element)
        {
             _CommerceContext.Update(element);
            await _CommerceContext.SaveChangesAsync();
        }

        public async Task Delete(T element)
        {
            _CommerceContext.Remove(element);
            await _CommerceContext.SaveChangesAsync();
        }
    }
}
