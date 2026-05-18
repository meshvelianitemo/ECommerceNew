
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Cart.Dtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Cart.Commands.MergeCarts
{
    public class MergeCartsCommand : IRequest<Result>
    {
        public List<AddToCartDto> CartItems { get; set; }
        public MergeCartsCommand(List<AddToCartDto> cartItems)
        {
            CartItems = cartItems;
        }
    
    }
}
