
using ECommerceNew.Application.Results.Errors;
using Microsoft.AspNetCore.Http;

namespace ECommerceNew.Application.Abstractions
{
    public interface IStorageRepoistory
    {
        Task<string> UploadFile( IFormFile file, CancellationToken cancelationToken);
        Task<Result<List<string?>>> GetImageUrl(int productId, CancellationToken cancelationToken);
        Task<Result> DeleteImageAsync(string url, CancellationToken cancellationToken);
    }
}
