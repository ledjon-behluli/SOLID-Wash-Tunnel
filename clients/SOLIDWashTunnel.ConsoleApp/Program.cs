using System;
using SOLIDWashTunnel.BuildingBlocks.DependecyInjection;
using SOLIDWashTunnel.Invoices;
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
            container.AddWashTunnel(car);

            IWashTunnel tunnel = container.GetService<IWashTunnel>();
            tunnel.SelectProgram(WashProgramFactory.GetProgram(ProgramType.Fast));
            tunnel.Wash(car);

            foreach (var step in car.AppliedWashSteps)
            {
                Console.WriteLine(step.Describe());
            }

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Console.WriteLine(tunnel.GetInvoiceForIndividual("Ledjon", "Behluli", Currency.EUR));
            Console.WriteLine(tunnel.GetInvoiceForIndividual("Ledjon", "Behluli", Currency.USD));
            Console.WriteLine(tunnel.GetInvoiceForCompany("SoftTech LLC", Currency.USD));

            Console.ReadKey();
        }
    }
}
