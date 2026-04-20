
namespace ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos
{
    public class CartItemDetailDto
    {
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Decimal? OriginalPrice { get; set; }
        public int Amount { get; set; }
        public string ProductCategoryName { get; set; } = string.Empty;
        public List<string> imageUrls { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;    
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
