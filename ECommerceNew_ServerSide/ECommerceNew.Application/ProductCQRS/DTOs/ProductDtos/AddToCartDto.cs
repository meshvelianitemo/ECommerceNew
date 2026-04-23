
namespace ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos
{
    public class AddToCartDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
    }
}
