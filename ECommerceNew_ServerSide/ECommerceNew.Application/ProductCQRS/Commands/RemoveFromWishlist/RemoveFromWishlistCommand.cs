using MediatR;
using Amazon.Runtime.Internal;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;

namespace ECommerceNew.Application.ProductCQRS.Commands.RemoveFromWishlist
{
    public class RemoveFromWishlistCommand : IRequest<Result>
    {
        public RemoveFromWishlistDto _Dto { get; set; }
        public RemoveFromWishlistCommand(RemoveFromWishlistDto dto)
        {
            _Dto = dto;
        }
    }
}
