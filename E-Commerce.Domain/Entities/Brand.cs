using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class Brand : BaseClass
    {
        [Required]
        [MaxLength(120)]
        public string Name { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
