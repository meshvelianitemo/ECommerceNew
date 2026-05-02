
namespace ECommerceNew.Application.Reviews.DTOs
{
    public class ReviewQueryParameters
    {
        public int ProductId { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
