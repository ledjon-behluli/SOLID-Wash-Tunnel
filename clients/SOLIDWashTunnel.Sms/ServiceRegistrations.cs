using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Programs.Steps;

namespace SOLIDWashTunnel.Sms
{
    public static class ServiceRegistrations
    {
        public static IContainer AddSmsNotifications(this IContainer container, string phoneNumber)
        {
            IWashStepNotifier notifier = container.GetService<IWashStepNotifier>();
            notifier.Subscribe(new SmsClient(phoneNumber));

            return container;
        }
    }
}
