using ECommerceNew.Application.Dashboard.DTOs;
using ECommerceNew.Application.Dashboard.Queries.GetCategoryRevenue;
using ECommerceNew.Application.Dashboard.Queries.GetLowStock;
using ECommerceNew.Application.Dashboard.Queries.GetOrderStatus;
using ECommerceNew.Application.Dashboard.Queries.GetRevenue;
using ECommerceNew.Application.Dashboard.Queries.GetTopCustomers;
using ECommerceNew.Application.Dashboard.Queries.GetTopProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ECommerceNew.Api.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    [EnableRateLimiting("token")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ISender sender, ILogger<DashboardController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenue(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            CancellationToken ct)
        {
            var result = await _sender.Send(new GetRevenueQuery(from, to), ct);
            return Ok(result);
        }

        [HttpGet("category-revenue")]
        public async Task<IActionResult> GetCategoryRevenue(CancellationToken ct)
        {
            var result = await _sender.Send(new GetCategoryRevenueQuery(), ct);
            return Ok(result);
        }

        [HttpGet("order-status")]
        public async Task<IActionResult> GetOrderStatus(CancellationToken ct)
        {
            var result = await _sender.Send(new GetOrderStatusQuery(), ct);
            return Ok(result);
        }

        [HttpGet("top-products")]
        public async Task<IActionResult> GetTopProducts(
            [FromQuery] int take = 5,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetTopProductsQuery(take), ct);
            return Ok(result);
        }

        [HttpGet("top-customers")]
        public async Task<IActionResult> GetTopCustomers(
            [FromQuery] int take = 5,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetTopCustomersQuery(take), ct);
            return Ok(result);
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockProducts(
            [FromQuery] int threshold = 5,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(new GetLowStockProductsQuery(threshold), ct);
            return Ok(result);
        }
    }
}
