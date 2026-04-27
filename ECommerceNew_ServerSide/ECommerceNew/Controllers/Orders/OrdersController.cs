using ECommerceNew.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNew.Api.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(ISender sender, ILogger<OrdersController> logger, IReviewRepository reviewRepository)
        {
            _logger = logger;
            _sender = sender;
        }



    }
}
