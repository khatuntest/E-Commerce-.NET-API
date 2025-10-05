using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(string search , decimal mnPrice , decimal mxPrice , string sort) 
        {
            // include Brand , Type 
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);


            //filter price range 
            if (mnPrice < 0) mnPrice = 0;
            if (mxPrice <= 0 || mxPrice < mnPrice) mxPrice = decimal.MaxValue;

            //Define condition with null-safe name filter
            Condition = p =>
                (string.IsNullOrEmpty(search) ||
                 (p.Name != null && p.Name.ToLower().Contains(search.ToLower()))) &&
                p.Price >= mnPrice && p.Price <= mxPrice;


            //sorting 
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(q => q.OrderBy(p => p.Price));
                        break;
                    case "pricedesc":
                        AddOrderBy(q => q.OrderByDescending(p => p.Price));
                        break;
                    case "nameasc":
                        AddOrderBy(q => q.OrderBy(p => p.Name));
                        break;
                    case "namedesc":
                        AddOrderBy(q => q.OrderByDescending(p => p.Name));
                        break;
                    default:
                        AddOrderBy(q => q.OrderBy(p => p.Id));
                        break;
                }
            }
            else
            {
                AddOrderBy(q => q.OrderBy(p => p.Id)); // default sort
            }
        }
    }
}
