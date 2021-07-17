using SOLIDWashTunnel.Programs;
using System;

namespace SOLIDWashTunnel.ClientFacing
{
    public interface IUserPanel
    {
        ICustomerInformationCollector SelectBuiltInProgram(ProgramType type);
        ICustomerInformationCollector SelectCustomizedProgram(IWashProgram program);
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
