using System;
using System.Collections.Generic;

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
        IPriceCalculator Create(CustomerType type);
    }

    public class PriceCalculatorFactory : IPriceCalculatorFactory
    {
        private readonly Lazy<IDictionary<CustomerType, Func<IPriceCalculator>>> _calculators;

        public PriceCalculatorFactory(IDictionary<CustomerType, Func<IPriceCalculator>> calculators)
        {
            _calculators = new Lazy<IDictionary<CustomerType, Func<IPriceCalculator>>>(calculators);
        }

        public IPriceCalculator Create(CustomerType type)
        {
            if (!_calculators.Value.TryGetValue(type, out Func<IPriceCalculator> _func))
            {
                throw new NotSupportedException($"No calculator was found for customer type = {type}");
            }

            return _func.Invoke();
        }
    }
}
