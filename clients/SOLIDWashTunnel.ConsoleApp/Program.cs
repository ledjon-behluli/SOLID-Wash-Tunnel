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

            var container = Container.Instance;
            container.Setup(car);

            IUserPanel panel = container.GetService<IUserPanel>();

            panel.SelectProgram(ProgramType.AllRounder)
                 .AsCompany("Ledjon SoftTech", Currency.EUR)
                 .Start(car, PrintInvoice());

            Console.ReadKey();
        }

        private static Action<string> PrintInvoice() => (content) =>
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(content);
        };
    }
}
