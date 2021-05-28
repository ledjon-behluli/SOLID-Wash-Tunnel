using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.SmsClient
{
    public static class ServiceRegistrations
    {
        public static IWashProcessStarter SubscribeToSmsMessages(this IWashProcessStarter starter)
        {
            starter.Subscribe(new SmsClient());
            return starter;
        }
    }
}