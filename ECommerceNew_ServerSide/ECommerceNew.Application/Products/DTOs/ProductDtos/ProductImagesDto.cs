
namespace ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos
{
    public class ProductImagesDto
    {
        public int productId { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
