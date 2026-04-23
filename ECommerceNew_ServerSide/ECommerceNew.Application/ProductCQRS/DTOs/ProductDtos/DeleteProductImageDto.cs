

namespace ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos
{
    public class DeleteProductImageDto
    {
        public int ProductId { get; set; } 
        public string ImageKey { get; set; } = string.Empty;
    }
}
