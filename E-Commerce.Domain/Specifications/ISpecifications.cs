using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Specifications
{
    public interface ISpecifications<T> where T : BaseClass
    {
        //Where
        public Expression<Func<T, bool>>? Criteria { get; set; }

        //Includes
        public List<Expression<Func<T, object>>> Includes { get; set; }

        //Sort Asc
        public Expression<Func<T, object>>? SortAsc { get; set; }

        //Sort Desc 
        public Expression<Func<T, object>>? SortDesc { get; set; }

        //Search 
        public Expression<Func<T , bool>>? Search {  get; set; }
    }
}
