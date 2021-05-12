using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Programs.Steps;
using System.Linq;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IWashTunnel
    {
        void Wash(IVehicle vehicle, IWashProgram program);
        //string GetInvoiceForIndividual(string firstName, string lastName, Currency preferedCurrecy);
        //string GetInvoiceForCompany(string companyName, Currency preferedCurrecy);
    }

    public class WashTunnel : IWashTunnel
    {
        private readonly ICentralControllerUnit _centralControllerUnit;

        public WashTunnel(ICentralControllerUnit centralControllerUnit)
        {
            _centralControllerUnit = centralControllerUnit;
        }

        public void Wash(IVehicle vehicle, IWashProgram program)
        {
            IWashStep[] washSteps = program.GetWashSteps().ToArray();

            for (int i = 0; i < washSteps.Length - 1; i++)
            {
                washSteps[i].NextStep(washSteps[i + 1]);
            }

            washSteps[0].Execute(vehicle);
            _centralControllerUnit.Transmit(Signal.VehicleReady);
        }

        //public string GetInvoiceForIndividual(string firstName, string lastName, Currency preferedCurrecy)
        //    => _invoiceBuilder
        //            .CreateForIndividual()
        //            .WithName(firstName, lastName)
        //            .Select(_program)
        //            .Choose(preferedCurrecy)
        //            .Calculate()
        //            .Print();

        //public string GetInvoiceForCompany(string companyName, Currency preferedCurrecy)
        //    => _invoiceBuilder
        //            .CreateForCompany()
        //            .WithName(companyName)
        //            .Select(_program)
        //            .Choose(preferedCurrecy)
        //            .Calculate()
        //            .Print();
    }
}
