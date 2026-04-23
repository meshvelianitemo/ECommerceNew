
namespace ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos
{
    public class ProductQueryParameters
    {

        public string? Search { get; set; }
        public int[]? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? Available { get; set; }
        public string? Sort { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
