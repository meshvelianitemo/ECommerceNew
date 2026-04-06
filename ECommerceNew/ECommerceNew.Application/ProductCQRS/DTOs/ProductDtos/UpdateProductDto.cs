
using ECommerceNew.Domain.Entities.ProductSide;

namespace ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Decimal Price { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    }
}
