using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.DI;
using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Tunnel;
using System;

namespace SOLIDWashTunnel.LightBulb
{
    

    class Program
    {
        static void Main(string[] args)
        {
            LightBulb lightBulb = new LightBulb();

            IContainer container = new SimpleContainer();
            ITunnelStateDispatcher dispatcher = container.GetService<ITunnelStateDispatcher>();
            dispatcher.Subscribe(lightBulb);

            Console.ReadKey();
        }
    }
}
