using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Tunnel.Steps;
using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.MobileApp
{
    public static class ServiceRegistrations
    {
        /// <summary>
        /// Subscribe to receive 'Mobile app notifications' on wash steps being applied to your registered <see cref="IVehicle"/>.
        /// </summary>
        public static IContainer AddMobileAppNotifications(this IContainer container, string username)
        {
            IWashStepNotifier notifier = container.GetService<IWashStepNotifier>();
            notifier.Subscribe(new MobileAppClient(username));

            return container;
        }
    }
}
