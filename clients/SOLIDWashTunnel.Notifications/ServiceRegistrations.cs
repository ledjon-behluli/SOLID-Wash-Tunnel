using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Programs.Steps;

namespace SOLIDWashTunnel.Notifications
{
    public static class ServiceRegistrations
    {
        public static IContainer AddSmsNotifications(this IContainer container, string phoneNumber)
        {
            IWashStepNotifier notifier = container.GetService<IWashStepNotifier>();
            notifier.Subscribe(new SmsClient(phoneNumber));

            return container;
        }

        public static IContainer AddMobileAppNotifications(this IContainer container, string username)
        {
            IWashStepNotifier notifier = container.GetService<IWashStepNotifier>();
            notifier.Subscribe(new MobileAppClient(username));

            return container;
        }
    }
}
