using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Tunnel;

namespace SOLIDWashTunnel.Control
{
    public class ControlUnitSignalHandler : 
        IControlUnitSignalHandler<WashProgramSelectedSignal>,
        IControlUnitSignalHandler<IndividualCustomerInfoEnteredSignal>,
        IControlUnitSignalHandler<CompanyCustomerInfoEnteredSignal>,
        IControlUnitSignalHandler<VehicleWashingStartedSignal>,
        IControlUnitSignalHandler<VehicleReadySignal>
    {
        private readonly IControlUnitMemory _memory;
        private readonly IWashTunnel _washTunnel;
        private readonly IInvoiceBuilder _invoiceBuilder;

        public ControlUnitSignalHandler(
            IControlUnitMemory memory,
            IWashTunnel washTunnel,
            IInvoiceBuilder invoiceBuilder)
        {
            _memory = memory;
            _washTunnel = washTunnel;
            _invoiceBuilder = invoiceBuilder;
        }


        public void Handle(WashProgramSelectedSignal signal)
            => _memory.SetOrOverride("WPSS", signal);
      
        public void Handle(IndividualCustomerInfoEnteredSignal signal)
            => _memory.SetOrOverride("ICIES", signal);

        public void Handle(CompanyCustomerInfoEnteredSignal signal)
            => _memory.SetOrOverride("CCIES", signal);

        public void Handle(VehicleWashingStartedSignal signal)
        {
            if (_memory.TryGet("WPSS", out WashProgramSelectedSignal _signal))
                _washTunnel.Wash(signal.Vehicle, _signal.Program);
        }

        public void Handle(VehicleReadySignal signal)
        {
            string report = GenerateInvoiceReport();
        }

        private string GenerateInvoiceReport()
        {
            IProgramSelector selector = null;
            CustomerInfoEnteredSignal info;

            if (_memory.TryGet("ICIES", out info))
            {
                var individualInfo = info as IndividualCustomerInfoEnteredSignal;

                selector = _invoiceBuilder
                    .CreateForIndividual()
                    .WithName(individualInfo.FirstName, individualInfo.LastName);
            }
            else if (_memory.TryGet("CCIES", out info))
            {
                var companyInfo = info as CompanyCustomerInfoEnteredSignal;

                selector = _invoiceBuilder
                    .CreateForCompany()
                    .WithName(companyInfo.CompanyName);
            }

            if (selector != null)
            {
                if (_memory.TryGet("WPSS", out WashProgramSelectedSignal _signal))
                {
                    return selector
                        .Select(_signal.Program)
                        .Choose(info.PreferredCurrency)
                        .Calculate()
                        .Build();
                }
            }

            return string.Empty;
        }
    }
}
