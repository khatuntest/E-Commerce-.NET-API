using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        
        public ProductSpecification(ProductQueryParametersDtos parameters) : base(parameters)
        {
            AddIncludes();

            //sort
            if (!string.IsNullOrEmpty(parameters.Sort))
            {
                Sorting(parameters.Sort);

            }
            else
            {
                SortAsc = p => p.Price;
            }

            //search
            if (!String.IsNullOrEmpty(parameters.Keyword))
            {
                Search = p => p.Name.ToLower().Contains(parameters.Keyword.ToLower())
                                || p.Description.ToLower().Contains(parameters.Keyword.ToLower());
            }

            //Filter
            if(parameters.MinPrice.HasValue && parameters.MaxPrice.HasValue)
            {
                Criteria = p => (p.Price <= parameters.MaxPrice.Value) && (p.Price >= parameters.MinPrice.Value);
            }

        }
        public ProductSpecification() 
            => AddIncludes();
       
        public ProductSpecification(int id) : base(p => p.Id == id) 
            => AddIncludes();

        private void AddIncludes()
        {
            Includes.Add(p=> p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }

        private void Sorting(string sort)
        {
            /*
            * will make types of search represented by numbers 
            * Price Asc ==>    1
            * Price Desc ==>   2
            * Name Asc   ==>   3
            * Name Desc  ==>   4
            */

            if (sort == "1")
            {
                SortAsc = p => p.Price;
            }
            else if (sort == "2")
            {
                SortDesc = p => p.Price;
            }
            else if (sort == "3")
            {
                SortAsc = p => p.Name;
            }
            else
                SortDesc = p => p.Name;
        }
    }
}
