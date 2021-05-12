using System;
using SOLIDWashTunnel.IoC;
using SOLIDWashTunnel.Tunnel;
using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car();

            var container = new Container();
            container.Setup(car);

            IUserPanel panel = container.GetService<IUserPanel>();

            panel.SelectProgram(ProgramType.Fast);
            panel.Start(car);

            foreach (var step in car.AppliedWashSteps)
            {
                Console.WriteLine(step.Describe());
            }

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Console.WriteLine(tunnel.GetInvoiceForIndividual("Ledjon", "Behluli", Currency.USD));

            Console.ReadKey();
        }
    }
}
