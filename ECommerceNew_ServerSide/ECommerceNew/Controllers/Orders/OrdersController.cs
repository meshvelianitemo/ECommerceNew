using ECommerceNew.Api.Controllers.Reviews;
using ECommerceNew.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ECommerceNew.Api.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<ReviewsController> _logger;
        private readonly 
        public OrdersController(ISender sender, ILogger<ReviewsController> logger, IReviewRepository reviewRepository)
        {
            _logger = logger;
            _sender = sender;
        }



    }
}
