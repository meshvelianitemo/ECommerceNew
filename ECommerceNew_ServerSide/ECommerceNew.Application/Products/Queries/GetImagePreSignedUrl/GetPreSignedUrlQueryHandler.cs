using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceNew.Application.ProductCQRS.Queries.GetImagePreSignedUrl
{
    public class GetPreSignedUrlQueryHandler : IRequestHandler<GetPreSignedUrlQuery, Result<List<string>>>
    {
        private readonly IStorageRepoistory _storageRepository;

        public GetPreSignedUrlQueryHandler(IStorageRepoistory storageRepository)
        {
            _storageRepository = storageRepository;
        }
        public async Task<Result<List<string>>> Handle(GetPreSignedUrlQuery request, CancellationToken cancellationToken)
        {
            var result = await _storageRepository.GetImageUrl(request.productId, cancellationToken);
            if (!result.IsSuccess)
            {
                return Result<List<string>>.Failure(ProductErrors.ImagesNotFound);
            }
            return Result<List<string>>.Success(result.Value);

        }
    }
}
