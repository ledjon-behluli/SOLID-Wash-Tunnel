using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Programs.Steps;
using System;

namespace SOLIDWashTunnel.MobileApp
{
    public class MobileAppClient : IWashStepSubscriber
    {
        private readonly string _username;

        public MobileAppClient(string username)
        {
            _username = username;
        }

        public void OnNewStepApplied(IWashStep step)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[MobileAppClient] [{_username}]: {step.GetDescription()}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
