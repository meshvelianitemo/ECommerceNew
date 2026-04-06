
using Amazon.Runtime.Internal;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Queries.GetImagePreSignedUrl
{
    public record GetPreSignedUrlQuery(int productId) : IRequest<List<string>> { }
}
