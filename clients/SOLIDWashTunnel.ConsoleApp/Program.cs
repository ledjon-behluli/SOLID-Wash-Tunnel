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
            Container.Instance.Setup(new Car());

            RunBuildInProgramForIndividual(ProgramType.Fast, "Ledjon", "Behluli", Currency.USD);
            RunBuildInProgramForIndividual(ProgramType.Fast, "Ledjon", "Behluli", Currency.EUR);
            RunBuildInProgramForIndividual(ProgramType.Economic, "Ledjon", "Behluli", Currency.USD);
            RunBuildInProgramForIndividual(ProgramType.Economic, "Ledjon", "Behluli", Currency.EUR);
            RunBuildInProgramForIndividual(ProgramType.AllRounder, "Ledjon", "Behluli", Currency.USD);
            RunBuildInProgramForIndividual(ProgramType.AllRounder, "Ledjon", "Behluli", Currency.EUR);

            RunBuildInProgramForCompany(ProgramType.Fast, "Ledjon SoftTech", Currency.USD);
            RunBuildInProgramForCompany(ProgramType.Fast, "Ledjon SoftTech", Currency.EUR);
            RunBuildInProgramForCompany(ProgramType.Economic, "Ledjon SoftTech", Currency.USD);
            RunBuildInProgramForCompany(ProgramType.Economic, "Ledjon SoftTech", Currency.EUR);
            RunBuildInProgramForCompany(ProgramType.AllRounder, "Ledjon SoftTech", Currency.USD);
            RunBuildInProgramForCompany(ProgramType.AllRounder, "Ledjon SoftTech", Currency.EUR);

            RunCustomWashProgramForIndividual("Ledjon", "Behluli", Currency.USD);
            RunCustomWashProgramForIndividual("Ledjon", "Behluli", Currency.EUR);

            RunCustomWashProgramForCompany("Ledjon SoftTech", Currency.USD);
            RunCustomWashProgramForCompany("Ledjon SoftTech", Currency.EUR);

            Console.ReadKey();
        }

        static void RunBuildInProgramForIndividual(ProgramType type, string firstName, string lastName, Currency currency)
        {
            var container = Container.Instance;

            var panel = container.GetService<IUserPanel>();
            var vehicle = container.GetService<IVehicle>();

            panel.SelectBuiltInProgram(type)
                 .AsIndividual(firstName, lastName, currency)
                 .Start(vehicle, PrintInvoice());
        }

        static void RunBuildInProgramForCompany(ProgramType type, string companyName, Currency currency)
        {
            var container = Container.Instance;

            var panel = container.GetService<IUserPanel>();
            var vehicle = container.GetService<IVehicle>();

            panel.SelectBuiltInProgram(type)
                 .AsCompany(companyName, currency)
                 .Start(vehicle, PrintInvoice());
        }

        static void RunCustomWashProgramForIndividual(string firstName, string lastName, Currency currency)
        {
            var container = Container.Instance;

            var panel = container.GetService<IUserPanel>();
            var vehicle = container.GetService<IVehicle>();
            var builder = container.GetService<ICustomWashProgramBuilder>();

            builder
                .AddChasisAndWheelWashing()
                .AddHighPressureWashing();

            panel.CustomizeProgram(builder)
                 .AsIndividual(firstName, lastName, currency)
                 .Start(vehicle, PrintInvoice());
        }

        static void RunCustomWashProgramForCompany(string companyName, Currency currency)
        {
            var container = Container.Instance;

            var panel = container.GetService<IUserPanel>();
            var vehicle = container.GetService<IVehicle>();
            var builder = container.GetService<ICustomWashProgramBuilder>();

            builder
                .AddChasisAndWheelWashing()
                .AddShampooing()
                .AddHighPressureWashing()
                .AddThreeColorFoaming()
                .AddHighPressureWashing()
                .AddAirDrying()
                .AddWaxing();

            panel.CustomizeProgram(builder)
                 .AsCompany(companyName, currency)
                 .Start(vehicle, PrintInvoice());
        }


        static Action<string> PrintInvoice() => (content) =>
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(content);
            Console.WriteLine("\n\n\n");
        };
    }
}
