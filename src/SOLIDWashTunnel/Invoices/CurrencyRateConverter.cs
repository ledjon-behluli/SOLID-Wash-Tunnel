using System;

namespace SOLIDWashTunnel.Invoices
{
    public interface ICurrencyRateConverter
    {
        decimal Convert(decimal amount, Currency currency);
    }

    // You would use an external service to handle currency convertion
    public class CurrencyRateConverter : ICurrencyRateConverter
    {
        public decimal Convert(decimal amount, Currency currency) =>
            currency switch
                {
                    Currency.USD => amount,              // All prices in the system are in USD, so we just return the amount as it is
                    Currency.EUR => 0.82m * amount,      // At the time of writing this, the conversation rate from USD -> EURO was 0.82
                    Currency.CAD => 1.21m * amount,      // At the time of writing this, the conversation rate from USD -> CAD was 1.21
                    Currency.JPY => 108.54m * amount,    // At the time of writing this, the conversation rate from USD -> JPY was 108.54
                    _ => throw new NotSupportedException()
                };
    }
}
