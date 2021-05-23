using System;

namespace SOLIDWashTunnel.WebServices
{
    public interface ILegacyCurrencyRateConverter
    {
        decimal Convert(string accessToken, decimal price, string currency);
    }

    // For production ready systems, you would use an external service provider to handle currency convertions
    internal class LegacyCurrencyRateConverter : ILegacyCurrencyRateConverter
    {
        private readonly ITokenRegistry _tokenRegistry;

        public LegacyCurrencyRateConverter(ITokenRegistry tokenRegistry)
        {
            _tokenRegistry = tokenRegistry;
        }

        public decimal Convert(string accessToken, decimal price, string currency)
        {
            if (!_tokenRegistry.IsValid(accessToken))
                throw new InvalidOperationException($"Access token {accessToken} is not valid.");

            return currency switch
            {
                "USD" => price,                  // All prices in the system are in USD, so we just return the amount as it is
                "EUR" => 0.82m * price,          // At the time of writing, the conversation rate from USD -> EURO was 0.82
                "CAD" => 1.21m * price,          // At the time of writing, the conversation rate from USD -> CAD was 1.21
                "JPY" => 108.54m * price,        // At the time of writing, the conversation rate from USD -> JPY was 108.54
                _ => throw new NotSupportedException(),
            };
        }
    }
}
