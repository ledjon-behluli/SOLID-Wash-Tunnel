using SOLIDWashTunnel.BuildingBlocks.DependecyInjection;
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Tunnels;
using SOLIDWashTunnel.WashPrograms;
using SOLIDWashTunnel.WashComponents;
using System;

namespace SOLIDWashTunnel
{
    public static class RegistrationExtensions
    {
        public static IContainer AddWashTunnel(this IContainer container, 
            Func<IVehicle> vehicleCreator)
        {
            container.Register(vehicleCreator);
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
