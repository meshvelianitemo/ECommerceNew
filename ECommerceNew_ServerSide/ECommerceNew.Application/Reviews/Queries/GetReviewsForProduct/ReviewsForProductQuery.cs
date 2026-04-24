using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceNew.Application.Reviews.Queries.GetReviewsForProduct
{
    public record ReviewsForProductQuery(int productId) : IRequest<Result<ReviewsForProduct>>;
}
