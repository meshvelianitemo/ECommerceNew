
using Amazon.Runtime.EventStreams;

namespace ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos
{
    public class RemoveFromWishlistDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }  
    }
}
