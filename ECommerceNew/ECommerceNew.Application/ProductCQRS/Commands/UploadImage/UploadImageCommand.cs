
using Amazon.Runtime.Internal;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.UploadImage
{
    public class UploadImageCommand : IRequest<Result>
    {
        public UploadImageDto _Dto { get; set; }
        public UploadImageCommand(UploadImageDto Dto)
        {
            _Dto = Dto;
        }
    }
}
