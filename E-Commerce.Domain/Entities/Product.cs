using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class Product : BaseClass
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        

        public int ProductTypeId { get; set; }
        public Types ProductType { get; set; } = null!;
        

        public int ProductBrandId { get; set; }
        public Brand ProductBrand { get; set;} = null!;
    }
}
