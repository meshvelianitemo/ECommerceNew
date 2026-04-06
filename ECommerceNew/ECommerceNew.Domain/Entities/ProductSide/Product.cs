using ECommerceNew.Domain.Entities.UserSide;

namespace ECommerceNew.Domain.Entities.ProductSide
{
    public class Product
    {
        public int ProductId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Decimal Price { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; } 
        public int CategoryId { get; set; } 
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        // Navigation 
        public ProductCategory? ProductCategory { get; set; }
        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<WishListItem> WishListItems { get; set; } = new List<WishListItem>();

    }
}
