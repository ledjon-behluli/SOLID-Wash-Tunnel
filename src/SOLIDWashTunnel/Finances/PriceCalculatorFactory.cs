using System;

namespace SOLIDWashTunnel.Finances
{
    /* 
    * Pattern: Factory Method
    * Reason: TODO
    * Learn more: https://refactoring.guru/design-patterns/strategy
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
