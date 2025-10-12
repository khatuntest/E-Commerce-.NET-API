using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Utilities
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ECommerceContext _CommerceContext;
        public Hashtable _Repositories {  get; set; }

        public UnitOfWork(ECommerceContext commerceContext) 
        {
            _CommerceContext = commerceContext;
            _Repositories = new Hashtable();
        }
        public IGenericRepository<T> Repository<T>() where T : BaseClass
        {
            var key = typeof(T).Name;

            if(!_Repositories.ContainsKey(key))
            {
                _Repositories.Add(key , new GenericRepository<T>(_CommerceContext));
            }

            return (IGenericRepository<T>) _Repositories[key];
        }
        public int Complete()
        {
            return _CommerceContext.SaveChanges();
        }

        public void Dispose()
        {
            _CommerceContext.Dispose();
        }
    }
}
