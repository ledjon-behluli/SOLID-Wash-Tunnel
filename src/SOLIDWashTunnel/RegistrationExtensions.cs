using SOLIDWashTunnel.IoC;
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Tunnel;
using SOLIDWashTunnel.Materials;
using SOLIDWashTunnel.Customers;
using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel
{
    public static class RegistrationExtensions
    {
        public static IContainer Setup(this IContainer container, IVehicle vehicle)
        {
            container.Register(vehicle);

            container.Register<ICurrencyRateConverter, CurrencyRateConverter>();
            container.Register<ICustomerPriceCalculatorFactory, CustomerPriceCalculatorFactory>();
            container.Register<IInvoiceBuilder, InvoiceBuilder>();
            container.Register<IUserPanel, UserPanel>();
            container.Register<IWashTunnel, WashTunnel>();
            container.Register<IWashProgramFactory, WashProgramFactory>();
            container.Register<IBackDoor, BackDoor>();

            container.Register<ICentralControllerUnit, CentralControllerUnit>();

            container.Register<IBrush, Brush>();
            container.Register<IDryer, AirDryer>();
            container.Register<IFoam, Foam>();
            container.Register<IShampoo, Shampoo>();
            container.Register<IWax, Wax>();

            return container;
        }
    }
}
