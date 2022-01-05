using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Tunnel.Steps;
using System;

namespace SOLIDWashTunnel.Sms
{
    public class SmsClient : IWashStepSubscriber
    {
        private readonly string _phoneNumber;

        public SmsClient(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public void OnStepApplied(IWashStep step)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SmsClient] [{_phoneNumber}] [APPLIED]: {step.GetDescription()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void OnStepSkipped(IWashStep step)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[SmsClient] [{_phoneNumber}] [SKIPPED]: {step.GetDescription()}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
