using ECommerceNew.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNew.Api.Controllers
{
    [ApiController]
    [Route("api/paypal")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(decimal amount)
        {
            //var orderId = await _paymentService.CreateOrder(amount, "USD");
            //return Ok(new { orderId });
            return Ok();
        }

        [HttpPost("capture/{orderId}")]
        public async Task<IActionResult> Capture(string orderId)
        {
            //var success = await _paymentService.CaptureOrder(orderId);
            //return Ok(new { success });
            return Ok();
        }
    }
}
