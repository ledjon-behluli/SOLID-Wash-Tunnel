using System;
using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.DI;
using SOLIDWashTunnel.DI.Abstractions;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Tunnel.Steps;
using SOLIDWashTunnel.Sms;
using SOLIDWashTunnel.MobileApp;

namespace SOLIDWashTunnel.Customers
{
    class Program
    {
        static readonly IContainer container = new Container();
        static IVehicle Vehicle => 
            //new DirtyMetallicCar();
          new DirtyMatteCar();
          //new CleanMetallicCar();
          //new CleanMatteCar();
          

        static void Main(string[] args)
        {
            container
                .AddWashTunnel()           
                .AddSmartFeatures()     
                .AddSmsNotifications("(917) 208-4154")
                .AddMobileAppNotifications("ledjon-behluli");

            //RunBuiltInProgramForIndividual(ProgramType.Fast, "Ledjon", "Behluli", Currency.USD);
            //RunBuiltInProgramForIndividual(ProgramType.Fast, "Ledjon", "Behluli", Currency.EUR);
            //RunBuiltInProgramForIndividual(ProgramType.Economic, "Ledjon", "Behluli", Currency.USD);
            //RunBuiltInProgramForIndividual(ProgramType.Economic, "Ledjon", "Behluli", Currency.EUR);
            //RunBuiltInProgramForIndividual(ProgramType.AllRounder, "Ledjon", "Behluli", Currency.USD);
            //RunBuiltInProgramForIndividual(ProgramType.AllRounder, "Ledjon", "Behluli", Currency.EUR);

            //RunBuiltInProgramForCompany(ProgramType.Fast, "Ledjon SoftTech", Currency.USD);
            //RunBuiltInProgramForCompany(ProgramType.Fast, "Ledjon SoftTech", Currency.EUR);
            //RunBuiltInProgramForCompany(ProgramType.Economic, "Ledjon SoftTech", Currency.USD);
            //RunBuiltInProgramForCompany(ProgramType.Economic, "Ledjon SoftTech", Currency.EUR);
            //RunBuiltInProgramForCompany(ProgramType.AllRounder, "Ledjon SoftTech", Currency.USD);
            //RunBuiltInProgramForCompany(ProgramType.AllRounder, "Ledjon SoftTech", Currency.EUR);

            RunCustomWashProgramForIndividual("Ledjon", "Behluli", Currency.USD);
            //RunCustomWashProgramForIndividual("Ledjon", "Behluli", Currency.EUR);

            //RunCustomWashProgramForCompany("Ledjon SoftTech", Currency.USD);
            //RunCustomWashProgramForCompany("Ledjon SoftTech", Currency.EUR);

            Console.ReadKey();
        }


        static void RunBuiltInProgramForIndividual(ProgramType type, string firstName, string lastName, Currency currency)
        {
            var panel = container.GetService<IUserPanel>();

            panel.SelectBuiltInProgram(type)
                 .AsIndividual(firstName, lastName, currency)
                 .Start(Vehicle, PrintInvoice());
        }

        static void RunBuiltInProgramForCompany(ProgramType type, string companyName, Currency currency)
        {
            var panel = container.GetService<IUserPanel>();

            panel.SelectBuiltInProgram(type)
                 .AsCompany(companyName, currency)
                 .Start(Vehicle, PrintInvoice());
        }

        static void RunCustomWashProgramForIndividual(string firstName, string lastName, Currency currency)
        {
            var panel = container.GetService<IUserPanel>();
            var builder = container.GetService<ICustomWashProgramBuilder>();

            var customProgram = builder
                .Add(WashStepType.ChasisAndWheelWashing)
                .Add(WashStepType.Shampooing)
                .Add(WashStepType.Waxing)
                .Add(WashStepType.HighPressureWashing)
                .Build();

            panel.SelectCustomizedProgram(customProgram)
                 .AsIndividual(firstName, lastName, currency)
                 .Start(Vehicle, PrintInvoice());
        }

        static void RunCustomWashProgramForCompany(string companyName, Currency currency)
        {
            var panel = container.GetService<IUserPanel>();
            var builder = container.GetService<ICustomWashProgramBuilder>();

            var customProgram = builder
                .AddAll()
                .Build();

            panel.SelectCustomizedProgram(customProgram)
                 .AsCompany(companyName, currency)
                 .Start(Vehicle, PrintInvoice());
        }


        static Action<string> PrintInvoice() => (content) =>
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("\nInvoice Report");
            Console.WriteLine("*************************");
            Console.WriteLine(content);
            Console.WriteLine("\n\n\n");
        };
    }
}
