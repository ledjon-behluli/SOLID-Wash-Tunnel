using System;
using SOLIDWashTunnel.IoC;
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Tunnel;
using SOLIDWashTunnel.Materials;
using SOLIDWashTunnel.Customers;
using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel
{
    public static class Extensions
    {
        public static IContainer Setup(this IContainer container, IVehicle vehicle)
        {
            container.Register(vehicle);

            container.RegisterSingleton<ICentralControllerUnit>(() => new CentralControllerUnit(container));

            container.Register<IUserPanel, UserPanel>();
            container.Register<IWashTunnel, WashTunnel>();
            container.Register<IBackDoor, BackDoor>();

            container.Register<ICurrencyRateConverter, CurrencyRateConverter>();
            container.Register<ICustomerPriceCalculatorFactory, CustomerPriceCalculatorFactory>();
            container.Register<IInvoiceBuilder, InvoiceBuilder>();
            container.Register<IWashProgramFactory, WashProgramFactory>();

            container.Register<IBrush, Brush>();
            container.Register<IDryer, AirDryer>();
            container.Register<IFoam, Foam>();
            container.Register<IShampoo, Shampoo>();
            container.Register<IWax, Wax>();

            return container;
        }

        public static string GetDescription<T>(this T @enum) where T : Enum
        {
            var field = @enum.GetType().GetField(@enum.ToString());
            var attributes = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false) as System.ComponentModel.DescriptionAttribute[];

            return attributes != null && attributes.Length > 0 ?
                attributes[0].Description : @enum.ToString();
        }
    }
}
