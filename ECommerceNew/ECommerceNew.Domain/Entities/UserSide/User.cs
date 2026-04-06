using ECommerceNew.Domain.Entities.ProductSide;
namespace ECommerceNew.Domain.Entities.UserSide
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public int RoleId { get; set; } 

        //Navigation property
        public Role Role { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public Cart Cart { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<WishListItem> WishListItems { get; set; } = new List<WishListItem>();
    }
}
