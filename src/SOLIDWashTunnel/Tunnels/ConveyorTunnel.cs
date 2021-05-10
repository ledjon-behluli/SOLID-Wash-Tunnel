using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.WashPrograms;
using SOLIDWashTunnel.WashPrograms.WashSteps;
using System.Linq;

namespace SOLIDWashTunnel.Tunnels
{
    public class ConveyorTunnel : IWashTunnel
    {
        private IWashProgram _program;
        private readonly IInvoiceBuilder _invoiceBuilder;

        public ConveyorTunnel(
            IWashProgram program,
            IInvoiceBuilder invoiceBuilder)
        {   
            _program = program;     // We inject a default wash program.
            _invoiceBuilder = invoiceBuilder;
        }

        public void SelectProgram(IWashProgram program)
        {
            _program ??= program;     // Wash program can be changed during runtime, if it is not null.
        }

        public void Wash(IVehicle vehicle)
        {
            IWashStep[] washSteps = _program.GetWashSteps().ToArray();

            for (int i = 0; i < washSteps.Length - 1; i++)
            {
                washSteps[i].NextStep(washSteps[i + 1]);
            }

            washSteps[0].Execute(vehicle);
        }

        public string GetInvoiceForIndividual(string firstName, string lastName, Currency preferedCurrecy)
            => _invoiceBuilder
                    .CreateForIndividual()
                    .WithName(firstName, lastName)
                    .Select(_program)
                    .Choose(preferedCurrecy)
                    .Calculate()
                    .Print();

        public string GetInvoiceForCompany(string companyName, Currency preferedCurrecy)
            => _invoiceBuilder
                    .CreateForCompany()
                    .WithName(companyName)
                    .Select(_program)
                    .Choose(preferedCurrecy)
                    .Calculate()
                    .Print();
    }
}
