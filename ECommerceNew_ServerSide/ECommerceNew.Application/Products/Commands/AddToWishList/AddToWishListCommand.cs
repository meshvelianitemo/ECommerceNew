
using Amazon.Runtime.Internal;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.AddToWishList
{
    public class AddToWishListCommand : IRequest<Result>
    {
        public AddToWishlistDto _Dto;

        public AddToWishListCommand(AddToWishlistDto dto)
        {
            _Dto = dto;  
        }
    }
}
