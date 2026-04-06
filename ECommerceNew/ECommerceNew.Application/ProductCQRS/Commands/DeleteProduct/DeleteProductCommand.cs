
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;


namespace ECommerceNew.Application.ProductCQRS.Commands.DeleteProduct
{
    public class DeleteProductCommand: IRequest<bool>
    {
        public DeleteProductDto _Dto { get; }
        public DeleteProductCommand(DeleteProductDto dto)
        {
            _Dto = dto;
        }
    }
}
