using SOLIDWashTunnel.BuildingBlocks.IoC;
using SOLIDWashTunnel.Auxiliaries;
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

            container.RegisterSingleton<IVehicleStatusPublisher>(() => new ControlPanel());

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


            container.GetService<IVehicleStatusPublisher>()
                     .Subscribe(new BackDoor())
                     .Subscribe(new InvoiceGenerator());

            return container;
        }
    }
}
