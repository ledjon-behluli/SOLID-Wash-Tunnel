using SOLIDWashTunnel.Invoices;
using System;

namespace SOLIDWashTunnel.Customers
{
    /* 
    * Pattern: Factory Method
    * Reason: TODO
    * Learn more: https://refactoring.guru/design-patterns/strategy
    */
    public interface ICustomerPriceCalculatorFactory
    {
        ICustomerPriceCalculator Create(CustomerType customerType);
    }

    public class CustomerPriceCalculatorFactory : ICustomerPriceCalculatorFactory
    {
        private readonly ICurrencyRateConverter converter;

        public CustomerPriceCalculatorFactory(ICurrencyRateConverter converter)
        {
            this.converter = converter;
        }

        public ICustomerPriceCalculator Create(CustomerType customerType) =>
            customerType switch
            {
                CustomerType.Individual => new IndividualPriceCalculator(converter),
                CustomerType.Company => new CompanyPriceCalculator(converter),
                _ => throw new NotSupportedException("Specified wash program is not available!"),
            };
    }
}
