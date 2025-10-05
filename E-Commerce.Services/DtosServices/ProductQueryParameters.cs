namespace E_Commerce.API.Dtos
{
    public class ProductQueryParameters
    {
        public int? PageNumber {  get; set; }
        public int? PageSize {  get; set; }
        public string? Sort {  get; set; }
        public string? Search {  get; set; }
    }
}
