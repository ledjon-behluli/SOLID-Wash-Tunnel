﻿using SOLIDWashTunnel.IoC;
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Tunnel;
using SOLIDWashTunnel.Finances;
using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;

namespace SOLIDWashTunnel
{
    public static class Registrations
    {
        public static IContainer Setup(this IContainer container, IVehicle vehicle)
        {
            container.Register(vehicle);

            container.RegisterSingleton<IMotherboard>(() => new Motherboard(container));
            container.RegisterSingleton<IMemory>(() => new Memory());

            container.Register<IUserPanel, UserPanel>();
            container.Register<IWashTunnel, WashTunnel>();

            container.Register<ICurrencyRateConverter, CurrencyRateConverter>();
            container.Register<IPriceCalculatorFactory, PriceCalculatorFactory>();
            container.Register<IInvoiceBuilder, InvoiceBuilder>();
            container.Register<IWashProgramFactory, WashProgramFactory>();

            return container;
        }
    }
}
