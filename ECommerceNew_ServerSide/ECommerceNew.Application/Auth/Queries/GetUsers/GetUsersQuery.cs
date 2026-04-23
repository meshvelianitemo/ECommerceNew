using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceNew.Application.Auth.Queries.GetUsers
{
    public record GetUsersQuery (UserQueryParameters queryParams) : IRequest<Result<PagedResult<UserInfoDto>>>;
}
