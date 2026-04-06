using ECommerceNew.Application.Abstractions;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceNew.Application.ProductCQRS.Queries.GetImagePreSignedUrl
{
    public class GetPreSignedUrlQueryHandler : IRequestHandler<GetPreSignedUrlQuery, List<string>>
    {
        private readonly IStorageRepoistory _storageRepository;

        public GetPreSignedUrlQueryHandler(IStorageRepoistory storageRepository)
        {
            _storageRepository = storageRepository;
        }
        public async Task<List<string>> Handle(GetPreSignedUrlQuery request, CancellationToken cancellationToken)
        {
            var urls = await _storageRepository.GetImageUrl(request.productId, cancellationToken);
            if (urls == null || urls.Count == 0)
            {
                throw new SecurityTokenException("No image found for the specified product.");
            }
            return urls;

        }
    }
}
