
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;


namespace ECommerceNew.Application.ProductCQRS.Commands.DeleteProduct
{
    public class DeleteProductCommand: IRequest<Result>
    {
        public DeleteProductDto _Dto { get; }
        public DeleteProductCommand(DeleteProductDto dto)
        {
            _Dto = dto;
        }
    }
}
