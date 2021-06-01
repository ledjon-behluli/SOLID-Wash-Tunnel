using SOLIDWashTunnel.Finances;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Programs.Steps;
using System;
using System.Collections.Generic;

namespace SOLIDWashTunnel
{
    internal static class ConfigMap
    {
        internal static IDictionary<ProgramType, Func<IWashStep[], IWashProgram>> GetWashPrograms() =>
            new Dictionary<ProgramType, Func<IWashStep[], IWashProgram>>()
                {
                    { ProgramType.Custom, (ws) => new CustomWashProgram(ws) },
                    { ProgramType.Fast, (ws) => new FastWashProgram() },
                    { ProgramType.Economic, (ws) => new EconomicWashProgram() },
                    { ProgramType.AllRounder, (ws) => new AllRounderWashProgram() }
                };

        internal static IDictionary<CustomerType, Func<IPriceCalculator>> GetPriceCalculators(ICurrencyRateConverter converter) =>
            new Dictionary<CustomerType, Func<IPriceCalculator>>()
                {
                    { CustomerType.Individual, () => new IndividualPriceCalculator(converter) },
                    { CustomerType.Company, () => new CompanyPriceCalculator(converter) }
                };
    }
}
