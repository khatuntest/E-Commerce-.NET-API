using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Specifications
{
    public class BaseSpecification<T> : ISpecifications<T> where T : BaseClass
    {
        
        public Expression<Func<T, bool>>? Criteria { get; set; }
        public Expression<Func<T, object>>? SortAsc { get; set; }
        public Expression<Func<T, object>>? SortDesc { get ; set; }
        public Expression<Func<T, bool>>? Search { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>(){ };

        //for Get All
        public BaseSpecification()
        {

        }

        public BaseSpecification(ProductQueryParametersDtos parameters)
        {

        }

        //for Get By Id
        public BaseSpecification(Expression<Func<T, bool>>? _criteria)
        {
            Criteria = _criteria;
        }





    }
}
