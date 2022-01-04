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

        public void OnNewStepApplied(IWashStep step)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[SmsClient] [{_phoneNumber}]: {step.GetDescription()}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
