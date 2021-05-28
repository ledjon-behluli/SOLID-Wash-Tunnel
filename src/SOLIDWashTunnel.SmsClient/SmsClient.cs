using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Programs.Steps;
using System;

namespace SOLIDWashTunnel.SmsClient
{
    internal class SmsClient : IWashStepSubscriber
    {
        public void OnStateChange(IWashStep step)
        {
            Console.WriteLine($"Sending SMS with content: Current wash step on vehicle is: '{step.GetDescription()}'");
        }
    }
}
