using SOLIDWashTunnel.BuildingBlocks.DependecyInjection;
using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel
{
    public static class RegistrationExtensions
    {
        public static IContainer AddWashTunnel(this IContainer container)
        {
            container.Register<IVehicle, Car>();


            return container;
        }
    }
}
