using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Tunnel;
using SOLIDWashTunnel.Finances;
using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;

namespace SOLIDWashTunnel
{
    public static class ServiceRegistrations
    {
        public static IContainer AddWashTunnel(this IContainer container)
        {
            container.RegisterSingleton<IMotherboard>(() => new Motherboard(container));
            container.RegisterSingleton<IMemory>(() => new Memory());

            container.Register<IUserPanel, UserPanel>();
            container.Register<IWashTunnel, WashTunnel>();

            container.Register<ICurrencyRateConverter, CurrencyRateConverter>();
            container.Register<IPriceCalculatorFactory, PriceCalculatorFactory>();
            container.Register<IInvoiceBuilder, InvoiceBuilder>();
            container.Register<ICustomWashProgramBuilder, CustomWashProgramBuilder>();
            container.Register<IWashProgramFactory, WashProgramFactory>();

            return container;
        }
    }
}
