using SOLIDWashTunnel.BuildingBlocks.DependecyInjection;
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Tunnels;
using SOLIDWashTunnel.WashPrograms;
using SOLIDWashTunnel.WashComponents;
using SOLIDWashTunnel.Customers;
using SOLIDWashTunnel.Invoices;

namespace SOLIDWashTunnel
{
    public static class RegistrationExtensions
    {
        public static IContainer AddWashTunnel(this IContainer container, IVehicle vehicle)
        {
            container.Register(vehicle);

            container.Register<ICurrencyRateConverter, CurrencyRateConverter>();
            container.Register<ICustomerPriceCalculatorFactory, CustomerPriceCalculatorFactory>();
            container.Register<IInvoiceBuilder, InvoiceBuilder>();
            container.Register<IWashTunnel, ConveyorTunnel>();
            container.Register<IWashProgram, FastWashProgram>();

            container.Register<IBrush, Brush>();
            container.Register<IDryer, AirDryer>();
            container.Register<IFoam, Foam>();
            container.Register<IShampoo, Shampoo>();
            container.Register<IWax, Wax>();

            return container;
        }
    }
}
