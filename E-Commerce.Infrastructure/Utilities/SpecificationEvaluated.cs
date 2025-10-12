using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Utilities
{
    public static class SpecificationEvaluated<T> where T : BaseClass
    {
        public static IQueryable<T> GetQuery(IQueryable<T> _query , ISpecifications<T> specs)
        {
            var query = _query;//DbContext.Set<T>()

            if(specs.Criteria != null)
            {
                query = query.Where(specs.Criteria);//DbContext.Set<T>().Where(p => p.Id == Id);
            }

            if(specs.SortAsc  != null)
            {
                query = query.OrderBy(specs.SortAsc);
            }

            if(specs.SortDesc != null)
            {
                query = query.OrderByDescending(specs.SortDesc);
            }

            if(specs.Includes != null)
            {
                query = specs.Includes.Aggregate(query , (current , include) => current.Include(include));
            }

            if(specs.Search != null)
            {
                query = query.Where(specs.Search);
            }


            return query;
        }
    }
}
