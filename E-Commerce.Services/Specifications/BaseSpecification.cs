using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    public class BaseSpecification<T>
    {
        //filter condition
        public Expression<Func<T,bool>> Condition {  get; set; }

        //which related to it 
        public List <Expression<Func<T, Object>>> Includes { get; } = new();

        //order by
        public Func<IQueryable<T> , IOrderedQueryable<T>> OrderBy { get; set; }

        public int? Skip { get; set; }
        public int? Take { get; set; }

        public void AddInclude(Expression<Func<T, Object>> expression)
        {
            Includes.Add(expression);
        }

        //add sorting
        public void AddOrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> order)
        {
            OrderBy = order;
        }

        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
