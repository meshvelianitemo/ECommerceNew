
using Amazon.Runtime.Internal;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.DeleteProductImage
{
    public class DeleteProductImageCommand : IRequest<bool>
    {
        public DeleteProductImageDto _Dto { get; }

        public DeleteProductImageCommand(DeleteProductImageDto dto)
        {
            _Dto = dto;
        }
    }
}
