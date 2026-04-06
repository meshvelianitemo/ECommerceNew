
using Microsoft.AspNetCore.Http;

namespace ECommerceNew.Application.Abstractions
{
    public interface IStorageRepoistory
    {
        Task<string> UploadFile( IFormFile file, CancellationToken cancelationToken);
        Task<List<string>> GetImageUrl(int productId, CancellationToken cancelationToken);
        Task<bool> DeleteImageAsync(string url, CancellationToken cancellationToken);
    }
}
