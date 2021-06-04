using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Legacy;
using SOLIDWashTunnel.Tunnel;
using SOLIDWashTunnel.Finances;
using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;
using SOLIDWashTunnel.Sensors;
using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Programs.Steps;

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
            container.RegisterSingleton<IMotherboard>(() => new Motherboard(container));
            container.RegisterSingleton<IMemory>(() => new RandomAccessMemory());
            container.RegisterSingleton<IWashStepNotifier>(() => new WashStepNotifier());

            container.Register<IUserPanel, UserPanel>();
            container.Register<IWashTunnel, WashTunnel>();

            ILegacyCurrencyRateConverter legacyConverter = LegacyCurrencyRateConverterProxy.Instance.Authenticate("solid-tunnel-00F1BDE0-AC18-452B-A628-B8FB0335DAB6");
            ICurrencyRateConverter converter = new CurrencyRateConverter(legacyConverter, ConfigMap.GetLegacyCurrencies());

            container.RegisterSingleton(() => converter);
            container.RegisterSingleton<IPriceCalculatorFactory>(() => new PriceCalculatorFactory(ConfigMap.GetPriceCalculators(converter)));

            container.Register<IInvoiceBuilder, InvoiceBuilder>();
            container.Register<ICustomWashProgramBuilder, CustomWashProgramBuilder>();
            container.RegisterSingleton<IWashProgramFactory>(() => new WashProgramFactory(ConfigMap.GetWashPrograms()));

            return container;
        }

        /// <summary>
        /// Registers all smart features of a smart wash tunnel.
        /// The tunnel will wash a vehicle only if it considered to be 'dirty'.
        /// </summary>
        public static IContainer AddSmartFeatures(this IContainer container)
        {
            container.Register(() => new DirtinessSensor().Calibrate(5));
            container.Decorate<IWashTunnel, SmartWashTunnel>();

            return container;
        }
    }
}
