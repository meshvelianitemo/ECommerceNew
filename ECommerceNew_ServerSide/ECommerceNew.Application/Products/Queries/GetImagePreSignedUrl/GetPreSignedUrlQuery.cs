
using Amazon.Runtime.Internal;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Queries.GetImagePreSignedUrl
{
    public record GetPreSignedUrlQuery(int productId) : IRequest<Result<List<string>>> { }
}
