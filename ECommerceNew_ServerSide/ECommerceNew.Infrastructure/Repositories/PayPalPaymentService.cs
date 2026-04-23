using ECommerceNew.Application.Abstractions;
using MailKit.Search;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ECommerceNew.Infrastructure.Repositories
{
    public class PayPalPaymentService //: IPaymentService
    {
        private readonly PaypalServerSdkClient _client;

        public PayPalPaymentService(PaypalServerSdkClient client)
        {
            _client = client;
        }

        //public async Task<string> CreateOrder(decimal amount, string currency)
        //{
        //    var input = new CreateOrderInput
        //    {
        //        Body = new
        //        {
        //            intent = "CAPTURE",
        //            purchase_units = new[]
        //            {
        //        new
        //        {
        //            amount = new
        //            {
        //                currency_code = currency,
        //                value = amount.ToString("F2")
        //            }
        //        }
        //    }
        //        }
        //    };

        //    var response = await _client.OrdersController.CreateOrderAsync(input);

        //    // response is NOT strongly typed → extract dynamically
        //    var result = response as dynamic;

        //    return result.id;
        //}

        //public async Task<bool> CaptureOrder(string orderId)
        //{
        //    var input = new CaptureOrderInput
        //    {
        //        Id = orderId
        //    };

        //    var response = await _client.OrdersController.CaptureOrderAsync(input);

        //    var json = response.RawBody;

        //    using var doc = JsonDocument.Parse(json);
        //    var root = doc.RootElement;

        //    var status = root.GetProperty("status").GetString();

        //    return status == "COMPLETED";
        //}
    }
}