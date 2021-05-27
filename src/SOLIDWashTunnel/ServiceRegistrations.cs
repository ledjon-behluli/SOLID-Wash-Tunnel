using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Legacy;
using SOLIDWashTunnel.Tunnel;
using SOLIDWashTunnel.Finances;
using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;
using SOLIDWashTunnel.Sensors;
using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel
{
    //TODO: Observer pattern: implement signal login in external provider or similar

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

            container.Register<IUserPanel, UserPanel>();
            container.Register<IWashTunnel, WashTunnel>();

            container.Register<ICurrencyRateConverter>(() =>
            {
                ILegacyCurrencyRateConverter legacyConverter = 
                    LegacyCurrencyRateConverterProxy.Instance.Authenticate("solid-tunnel-00F1BDE0-AC18-452B-A628-B8FB0335DAB6");

                return new CurrencyRateConverter(legacyConverter);
            });
            container.Register<IPriceCalculatorFactory, PriceCalculatorFactory>();
            container.Register<IInvoiceBuilder, InvoiceBuilder>();
            container.Register<ICustomWashProgramBuilder, CustomWashProgramBuilder>();
            container.Register<IWashProgramFactory, WashProgramFactory>();

            return container;
        }

        /// <summary>
        /// Registers all components needed for a smart wash tunnel.
        /// The tunnel will wash a vehicle only if it considered to be 'dirty'.
        /// </summary>
        public static IContainer AddSmartWashTunnel(this IContainer container)
        {
            container.Dispose();
            container.AddWashTunnel();

            container.Register(() => new DirtinessSensor().Calibrate(5));
            container.Decorate<IWashTunnel, SmartWashTunnel>();

            return container;
        }

    }
}
