
using Microsoft.AspNetCore.Http;

namespace ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos
{
    public class UploadImageDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public IFormFile Image { get; set; } = null!;
    }
}
