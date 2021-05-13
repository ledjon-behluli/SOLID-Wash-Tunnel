using System;
using SOLIDWashTunnel.IoC;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Tunnel;
using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IVehicle car = new Car();

            var container = new Container();
            container.Setup(car);

            IUserPanel panel = container.GetService<IUserPanel>();

            panel.SelectProgram(ProgramType.Fast)
                 .ForCompany("asda", Invoices.Currency.EUR)
                 .Start(car);

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
