using System;
using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.DI;
using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel.ConsoleApp
{
    class Program
    {
        static readonly IContainer container = new SimpleContainer();
        static readonly IVehicle vehicle = 
            new DirtyCar(); 
         // new CleanCar();

        static void Main(string[] args)
        {
            // Choose per-liking

            container.AddWashTunnel();           // A normal wash tunnel registration (hover over it to see what it means)
            container.AddSmartWashTunnel();      // A smart wash tunnel registration (hover over it to see what it means)

            test(ProgramType.Fast, "Ledjon", "Behluli", Currency.USD);

            //RunBuildInProgramForIndividual(ProgramType.Fast, "Ledjon", "Behluli", Currency.USD);
            //RunBuildInProgramForIndividual(ProgramType.Fast, "Ledjon", "Behluli", Currency.EUR);
            //RunBuildInProgramForIndividual(ProgramType.Economic, "Ledjon", "Behluli", Currency.USD);
            //RunBuildInProgramForIndividual(ProgramType.Economic, "Ledjon", "Behluli", Currency.EUR);
            //RunBuildInProgramForIndividual(ProgramType.AllRounder, "Ledjon", "Behluli", Currency.USD);
            //RunBuildInProgramForIndividual(ProgramType.AllRounder, "Ledjon", "Behluli", Currency.EUR);
            
            //RunBuildInProgramForCompany(ProgramType.Fast, "Ledjon SoftTech", Currency.USD);
            //RunBuildInProgramForCompany(ProgramType.Fast, "Ledjon SoftTech", Currency.EUR);
            //RunBuildInProgramForCompany(ProgramType.Economic, "Ledjon SoftTech", Currency.USD);
            //RunBuildInProgramForCompany(ProgramType.Economic, "Ledjon SoftTech", Currency.EUR);
            //RunBuildInProgramForCompany(ProgramType.AllRounder, "Ledjon SoftTech", Currency.USD);
            //RunBuildInProgramForCompany(ProgramType.AllRounder, "Ledjon SoftTech", Currency.EUR);
            
            //RunCustomWashProgramForIndividual("Ledjon", "Behluli", Currency.USD);
            //RunCustomWashProgramForIndividual("Ledjon", "Behluli", Currency.EUR);
            
            //RunCustomWashProgramForCompany("Ledjon SoftTech", Currency.USD);
            //RunCustomWashProgramForCompany("Ledjon SoftTech", Currency.EUR);

            Console.ReadKey();
        }

        static void test(ProgramType type, string firstName, string lastName, Currency currency)
        {
            var panel = container.GetService<IUserPanel>();

            panel.SelectBuiltInProgram(type)
                 .AsIndividual(firstName, lastName, currency)
                 .Start(vehicle, PrintInvoice());
        }


        static void RunBuildInProgramForIndividual(ProgramType type, string firstName, string lastName, Currency currency)
        {
            var panel = container.GetService<IUserPanel>();

            panel.SelectBuiltInProgram(type)
                 .AsIndividual(firstName, lastName, currency)
                 .Start(vehicle, PrintInvoice());
        }

        static void RunBuildInProgramForCompany(ProgramType type, string companyName, Currency currency)
        {
            var panel = container.GetService<IUserPanel>();

            panel.SelectBuiltInProgram(type)
                 .AsCompany(companyName, currency)
                 .Start(vehicle, PrintInvoice());
        }

        static void RunCustomWashProgramForIndividual(string firstName, string lastName, Currency currency)
        {
            var panel = container.GetService<IUserPanel>();
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
            var panel = container.GetService<IUserPanel>();
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
