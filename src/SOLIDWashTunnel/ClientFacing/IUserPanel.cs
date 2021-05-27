using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Vehicles;
using System;

namespace SOLIDWashTunnel.ClientFacing
{
    public interface IUserPanel
    {
        ICustomerInformationCollector SelectBuiltInProgram(ProgramType type);
        ICustomerInformationCollector CustomizeProgram(ICustomWashProgramBuilder builder);
    }

    public interface ICustomerInformationCollector
    {
        IWashProcessStarter AsIndividual(string firstName, string lastName, Currency preferedCurrecy);
        IWashProcessStarter AsCompany(string companyName, Currency preferedCurrecy);
    }

    public interface IWashProcessStarter
    {
        void Start(IVehicle vehicle, Action<string> invoiceCallback);
    }
}
