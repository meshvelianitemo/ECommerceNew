using MediatR;
using Amazon.Runtime.Internal;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;

namespace ECommerceNew.Application.ProductCQRS.Commands.RemoveFromWishlist
{
    public class RemoveFromWishlistCommand : IRequest<bool>
    {
        public RemoveFromWishlistDto _Dto { get; set; }
        public RemoveFromWishlistCommand(RemoveFromWishlistDto dto)
        {
            _Dto = dto;
        }
    }
}
