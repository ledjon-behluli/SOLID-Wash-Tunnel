using System;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Finances
{
    /* 
    * Pattern: 
    *   Simple Factory
    *   
    * Reason: 
    *   Create a price calculator based on the customer type.
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
        private readonly Lazy<IDictionary<CustomerType, Func<IPriceCalculator>>> _calculatorsMap;

        public PriceCalculatorFactory(IDictionary<CustomerType, Func<IPriceCalculator>> calculators)
        {
            _calculatorsMap = new Lazy<IDictionary<CustomerType, Func<IPriceCalculator>>>(calculators);
        }

        public IPriceCalculator Create(CustomerType type)
        {
            if (!_calculatorsMap.Value.TryGetValue(type, out Func<IPriceCalculator> _func))
            {
                throw new NotSupportedException($"No calculator was found for customer type {type}");
            }

            return _func.Invoke();
        }
    }
}
