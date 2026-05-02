namespace ECommerceNew.Application.Cart.Dtos
{
    public class AddToCartDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
    }
}
