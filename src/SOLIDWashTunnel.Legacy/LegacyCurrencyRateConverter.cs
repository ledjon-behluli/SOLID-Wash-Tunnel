using System;

namespace SOLIDWashTunnel.Legacy
{
    public interface ILegacyCurrencyRateConverter
    {
        decimal Convert(decimal price, string currency);
    }

    internal class LegacyCurrencyRateConverter : ILegacyCurrencyRateConverter
    {
        public decimal Convert(decimal price, string currency)
            => currency switch
            {
                "USD" => price,                  // All prices in the system are in USD, so we just return the amount as it is
                "EUR" => 0.82m * price,          // At the time of writing, the conversation rate from USD -> EURO was 0.82
                "CAD" => 1.21m * price,          // At the time of writing, the conversation rate from USD -> CAD was 1.21
                "JPY" => 108.54m * price,        // At the time of writing, the conversation rate from USD -> JPY was 108.54
                _ => throw new NotSupportedException(),
            };
    }
}
