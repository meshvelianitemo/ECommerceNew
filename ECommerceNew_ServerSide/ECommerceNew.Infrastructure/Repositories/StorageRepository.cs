using Amazon.S3;
using Amazon.S3.Model;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ECommerceNew.Infrastructure.Repositories
{
    public class StorageRepository : IStorageRepoistory
    {

        private readonly AmazonS3Client _client;
        private readonly IProductRepository _productRepository;
        private readonly string _bucketName;
        private readonly string _region;

        public StorageRepository(IConfiguration config, IProductRepository repo, ILogger<StorageRepository> logger)
        {
            var accessKey = config["AWS:AccessKey"];
            var secretKey = config["AWS:SecretKey"];
            var region = Amazon.RegionEndpoint.EUNorth1;

            _bucketName = config["AWS:Bucket"];
            _region = config["AWS:Region"];

            _productRepository = repo;
            _client = new AmazonS3Client(accessKey, secretKey, region);
        }

        public async Task<Result> DeleteImageAsync(string key, CancellationToken cancellationToken)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = key
            };

            await _client.DeleteObjectAsync(request, cancellationToken);
            return Result.Success();

        }

        public async Task<Result<List<string?>>> GetImageUrl(int productId, CancellationToken cancelationToken)
        {
            var result = await _productRepository.ExtractImageUrl(productId, cancelationToken);

            if (!result.IsSuccess)
                return result;

            List<string?> preSignedUrls = new List<string?>();

            for (int i = 0; i < result.Value.Count; i++)
            {
                 var request = new GetPreSignedUrlRequest
                {
                    BucketName = _bucketName,
                    Key = result.Value[i],
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                var url = _client.GetPreSignedURL(request);
                preSignedUrls.Add(url);
            }
           
            return Result<List<string>>.Success(preSignedUrls);


        }

        public async Task<string> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var key = $"ProductImages/{Guid.NewGuid()}_{file.FileName}";

            using var stream = file.OpenReadStream();

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType
            };
            


            await _client.PutObjectAsync(request);

            var imageUrl = $"https://{_bucketName}.s3.{_region}.amazonaws.com/{key}";

            return key;
        }
    }
}
