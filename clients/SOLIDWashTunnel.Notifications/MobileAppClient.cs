using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Programs.Steps;
using System;

namespace SOLIDWashTunnel.Notifications
{
    public class MobileAppClient : IWashStepSubscriber
    {
        private readonly string _username;

        public MobileAppClient(string username)
        {
            _username = username;
        }

        public void OnStateChange(IWashStep step)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[MobileAppClient] [{_username}]: {step.GetDescription()}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
