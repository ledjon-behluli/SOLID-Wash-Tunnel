using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Legacy;
using SOLIDWashTunnel.Tunnel;
using SOLIDWashTunnel.Finances;
using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;
using SOLIDWashTunnel.Sensors;
using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Tunnel.Steps;

namespace SOLIDWashTunnel
{
    public static class ServiceRegistrations
    {
        /// <summary>
        /// Registers all components needed for a normal wash tunnel.
        /// The tunnel will wash a vehicle regardless if it is 'clean' or 'dirty'.
        /// </summary>
        public static IContainer AddWashTunnel(this IContainer container)
        {
            container.AddSingleton<ISignalTransmitter, Motherboard>();
            container.AddSingleton<IMemory, RandomAccessMemory>();
            container.AddSingleton<IWashStepNotifier, WashStepNotifier>();

            container.AddTransient<IUserPanel, UserPanel>();
            container.AddTransient<IWashTunnel, WashTunnel>();

            ILegacyCurrencyRateConverter legacyConverter = LegacyCurrencyRateConverterProxy.Instance.Authenticate("solid-tunnel-00F1BDE0-AC18-452B-A628-B8FB0335DAB6");
            ICurrencyRateConverter converter = new CurrencyRateConverter(legacyConverter, ConfigMap.GetLegacyCurrencies());

            container.AddSingleton(() => converter);
            container.AddSingleton<IPriceCalculatorFactory>(() => new PriceCalculatorFactory(ConfigMap.GetPriceCalculators(converter)));

            container.AddTransient<IInvoiceBuilder, InvoiceBuilder>();
            container.AddTransient<ICustomWashProgramBuilder, CustomWashProgramBuilder>();

            IWashStepFactory washStepFactory = new WashStepFactory(ConfigMap.GetWashSteps());
            container.AddSingleton(() => washStepFactory);
            container.AddSingleton<IWashProgramFactory>(() => new WashProgramFactory(ConfigMap.GetWashPrograms(washStepFactory)));

            return container;
        }

        /// <summary>
        /// Registers all smart features of a smart wash tunnel.
        /// The tunnel will wash a vehicle only if it considered to be 'dirty'.
        /// </summary>
        public static IContainer AddSmartFeatures(this IContainer container)
        {
            container.AddTransient(() => new DirtinessSensor().Calibrate(5));
            container.Decorate<IWashTunnel, SmartWashTunnel>();

            return container;
        }
    }
}
