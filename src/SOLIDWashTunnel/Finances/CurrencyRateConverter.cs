using System;

namespace SOLIDWashTunnel.Finances
{
    public interface ICurrencyRateConverter
    {
        Money Convert(Money price, Currency currency);
    }

    // You would use an external service to handle currency convertions
    public class CurrencyRateConverter : ICurrencyRateConverter
    {
        public Money Convert(Money price, Currency currency)
        {
            Money convertedPrice = currency switch
            {
                Currency.USD => price,                  // All prices in the system are in USD, so we just return the amount as it is
                Currency.EUR => 0.82m * price,          // At the time of writing, the conversation rate from USD -> EURO was 0.82
                Currency.CAD => 1.21m * price,          // At the time of writing, the conversation rate from USD -> CAD was 1.21
                Currency.JPY => 108.54m * price,        // At the time of writing, the conversation rate from USD -> JPY was 108.54
                _ => throw new NotSupportedException(),
            };

            return new Money(currency, convertedPrice.Amount);
        }
    }
}
