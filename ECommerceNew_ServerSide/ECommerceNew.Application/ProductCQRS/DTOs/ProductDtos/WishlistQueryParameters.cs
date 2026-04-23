
namespace ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos
{
    public class WishlistQueryParameters
    {
        public int UserId { get; set;  } 
        public bool? Available { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
