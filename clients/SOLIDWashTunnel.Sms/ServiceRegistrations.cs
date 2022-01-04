using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Tunnel.Steps;
using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Sms
{
    public static class ServiceRegistrations
    {
        /// <summary>
        /// Subscribe to receive 'SMS notifications' on wash steps being applied to your registered <see cref="IVehicle"/>.
        /// </summary>
        public static IContainer AddSmsNotifications(this IContainer container, string phoneNumber)
        {
            IWashStepNotifier notifier = container.GetService<IWashStepNotifier>();
            notifier.Subscribe(new SmsClient(phoneNumber));

            return container;
        }
    }
}
