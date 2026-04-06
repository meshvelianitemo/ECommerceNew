
using Amazon.Runtime.Internal;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.AddToWishList
{
    public class AddToWishListCommand : IRequest<bool>
    {
        public AddToWishlistDto _Dto;

        public AddToWishListCommand(AddToWishlistDto dto)
        {
            _Dto = dto;  
        }
    }
}
