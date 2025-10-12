

namespace E_Commerce.Domain.Entities
{
    public class ProductQueryParametersDtos
    {
        public string? Sort {  get; set; }
        public string? Keyword {  get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set;}
    }
}
