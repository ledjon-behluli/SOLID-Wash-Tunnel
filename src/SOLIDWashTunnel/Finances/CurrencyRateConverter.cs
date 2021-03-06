using System;
using System.Collections.Generic;
using SOLIDWashTunnel.Legacy;

namespace SOLIDWashTunnel.Finances
{
    /* 
    * Pattern: 
    *   Adapter
    *
    * Reason: 
    *   Converts the interface of a class into another interface that a clients expects. 
    *   Adapter lets classes work together that couldn't otherwise, because of incompatible interfaces.
    *   
    *   We are using the 'Money' value-object to represent money, but the legacy converter accepts a 'decimal' (primitive obsession) as the price.
    *   Also in our system we have a dedicated 'Currency' enum, but the legacy converter accepts a 'string' (primitive obsession) as the currency.
    *   The legacy converter has the logic to do the neccessary conversions between currencies, but our system is working with different types.
    *   We can no directly use the interface provided by the legacy converter, so we use the adapter pattern to adapt the 'ILegacyCurrencyRateConverter'
    *   to our 'ICurrencyRateConverter'.
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Adapter_pattern
    *   https://wiki.c2.com/?PrimitiveObsession
    */

    public interface ICurrencyRateConverter
    {
        Money Convert(Money price, Currency currency);
    }

    public class CurrencyRateConverter : ICurrencyRateConverter
    {
        private readonly ILegacyCurrencyRateConverter _legacyConverter;
        private readonly IDictionary<Currency, string> _currencies;

        public CurrencyRateConverter(
            ILegacyCurrencyRateConverter legacyConverter,
            IDictionary<Currency, string> currencies)
        {
            _legacyConverter = legacyConverter;
            _currencies = currencies;
        }

        public Money Convert(Money price, Currency currency)
        {
            if (!_currencies.ContainsKey(currency))
            {
                throw new NotSupportedException($"Currency type {currency} is not supported!");
            }

            var legacyFormat = _currencies[currency];
            var convertedAmount = _legacyConverter.Convert(price.Amount, legacyFormat);

            return new Money(currency, convertedAmount);
        }
    }
}
