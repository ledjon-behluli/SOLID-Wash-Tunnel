using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Programs.Steps;

namespace SOLIDWashTunnel.MobileApp
{
    public static class ServiceRegistrations
    {
        public static IContainer AddMobileAppNotifications(this IContainer container, string username)
        {
            IWashStepNotifier notifier = container.GetService<IWashStepNotifier>();
            notifier.Subscribe(new MobileAppClient(username));

            return container;
        }
    }
}
