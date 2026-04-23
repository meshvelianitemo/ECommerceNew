using Microsoft.Extensions.Configuration;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Authentication;

namespace ECommerceNew.Infrastructure
{
    public class PayPalClientFactory
    {

        public static PaypalServerSdkClient Create(Microsoft.Extensions.Configuration.IConfiguration config)
        {
            var clientId = config["PaypalServerSdk:ClientId"];
            var secret = config["PaypalServerSdk:ClientSecret"];
            var environment = config["PaypalServerSdk:Environment"] == "Production"
                ? PaypalServerSdk.Standard.Environment.Production
                : PaypalServerSdk.Standard.Environment.Sandbox;

            return new PaypalServerSdkClient.Builder()
                .ClientCredentialsAuth(
                    new ClientCredentialsAuthModel.Builder(
                        clientId,
                        secret
                    ).Build()
                )
                .Environment(environment)
                .Build();
        }
    }
}
