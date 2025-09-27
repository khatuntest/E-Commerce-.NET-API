using E_Commerce.Domain.Entities;

namespace E_Commerce.API.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public string ProductBrand { get; set; } = null!;
    }
}
