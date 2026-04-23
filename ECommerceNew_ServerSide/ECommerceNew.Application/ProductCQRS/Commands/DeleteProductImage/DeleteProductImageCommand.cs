
using Amazon.Runtime.Internal;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.DeleteProductImage
{
    public class DeleteProductImageCommand : IRequest<Result>
    {
        public DeleteProductImageDto _Dto { get; }

        public DeleteProductImageCommand(DeleteProductImageDto dto)
        {
            _Dto = dto;
        }
    }
}
