using SOLIDWashTunnel.Finances;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Programs.Steps;
using System;
using System.Collections.Generic;

namespace SOLIDWashTunnel
{
    internal static class ConfigMap
    {
        internal static IDictionary<ProgramType, Func<IWashStep[], IWashProgram>> GetWashPrograms(IWashStepFactory factory) =>
            new Dictionary<ProgramType, Func<IWashStep[], IWashProgram>>()
                {
                    { ProgramType.Custom, (ws) => new CustomWashProgram(ws) },
                    { ProgramType.Fast, _ => new FastWashProgram(factory) },
                    { ProgramType.Economic, _ => new EconomicWashProgram(factory) },
                    { ProgramType.AllRounder, _ => new AllRounderWashProgram(factory) }
                };

        internal static IDictionary<WashStepType, Func<IWashStep>> GetWashSteps() =>
            new Dictionary<WashStepType, Func<IWashStep>>()
            {
                { WashStepType.ChasisAndWheelWashing, () => new ChasisAndWheelWashing() },
                { WashStepType.Shampooing, () => new Shampooing() },
                { WashStepType.HighPressureWashing, () => new HighPressureWashing() },
                { WashStepType.SingleColorFoaming, () => new SingleColorFoaming() },
                { WashStepType.ThreeColorFoaming, () => new ThreeColorFoaming() },
                { WashStepType.Waxing, () => new Waxing() },
                { WashStepType.AirDrying, () => new AirDrying() }
            };

        internal static IDictionary<CustomerType, Func<IPriceCalculator>> GetPriceCalculators(ICurrencyRateConverter converter) =>
            new Dictionary<CustomerType, Func<IPriceCalculator>>()
                {
                    { CustomerType.Individual, () => new IndividualPriceCalculator(converter) },
                    { CustomerType.Company, () => new CompanyPriceCalculator(converter) }
                };

        internal static IDictionary<Currency, string> GetLegacyCurrencies() =>
            new Dictionary<Currency, string>()
            {
                 { Currency.USD, "USD" },
                 { Currency.EUR, "EUR" },
                 { Currency.CAD, "CAD" },
                 { Currency.JPY, "JPY" }
            };
    }
}
