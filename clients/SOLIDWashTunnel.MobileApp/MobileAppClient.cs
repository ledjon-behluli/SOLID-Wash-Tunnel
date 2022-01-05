using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Tunnel.Steps;
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

        public void OnStepApplied(IWashStep step)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[MobileAppClient] [{_username}] [APPLIED]: {step.GetDescription()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void OnStepSkipped(IWashStep step)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[MobileAppClient] [{_username}] [SKIPPED]: {step.GetDescription()}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
