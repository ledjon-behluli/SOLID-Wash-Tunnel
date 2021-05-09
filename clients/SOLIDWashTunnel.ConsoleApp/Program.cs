using System;
using SOLIDWashTunnel.BuildingBlocks.DependecyInjection;
using SOLIDWashTunnel.Tunnels;
using SOLIDWashTunnel.WashPrograms;

namespace SOLIDWashTunnel.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car();

            var container = new Container();
            container.AddWashTunnel(() => car);

            IWashTunnel tunnel = container.GetService<IWashTunnel>();
            tunnel.SelectProgram(WashProgramFactory.GetProgram(ProgramType.Fast));
            tunnel.Wash(car);

            foreach (var step in car.AppliedWashSteps)
            {
                Console.WriteLine(step.Describe());
            }

            Console.ReadKey();
        }
    }
}
