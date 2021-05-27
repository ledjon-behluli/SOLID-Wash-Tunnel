using System;

namespace SOLIDWashTunnel.Finances
{
    /* 
    * Pattern: 
    *   Simple Factory
    *   
    * Reason: 
    *   Decouple the retrival of a price calculator based on the customer type.
    *   Not to be confused with Factory Method or Abstract Factory patterns.
    *   
    * Learn more: 
    *   https://refactoring.guru/design-patterns/factory-comparison
    */

    public interface IPriceCalculatorFactory
    {
        IPriceCalculator Create(CustomerType customerType);
    }

    public class PriceCalculatorFactory : IPriceCalculatorFactory
    {
        private readonly ICurrencyRateConverter converter;

        public PriceCalculatorFactory(ICurrencyRateConverter converter)
        {
            this.converter = converter;
        }

        public IPriceCalculator Create(CustomerType customerType) =>
            customerType switch
            {
                CustomerType.Individual => new IndividualPriceCalculator(converter),
                CustomerType.Company => new CompanyPriceCalculator(converter),
                _ => throw new NotSupportedException()
            };
    }
}
