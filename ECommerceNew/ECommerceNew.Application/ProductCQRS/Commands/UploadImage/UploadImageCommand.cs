
using Amazon.Runtime.Internal;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Commands.UploadImage
{
    public class UploadImageCommand : IRequest<bool>
    {
        public UploadImageDto _Dto { get; set; }
        public UploadImageCommand(UploadImageDto Dto)
        {
            _Dto = Dto;
        }
    }
}
