using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Tunnel;

namespace SOLIDWashTunnel.Control
{
    /* 
     * Pattern: 
     *   Command
     *   
     * Reason: 
     *   Encapsulate all information needed to perform an action, or react to an event.
     *   
     * Thinking process:
     *   In general, it is recommended to sepparate command handlers into sepparate classes.
     *   In this example we are modeling an electronic device (CPU) which reacts on events/signals sent by the mediator (Motherboard), with appropriate handlers/actions.
     *   Since the CPU in the real-world is a single device, we are treating it here as such also. 
     *   Although it doesn't have to be like this!!!
     *   
     *   The injected dependencies reflect the real-word usage of a CPU:
     *      IMemory -
     *          You can swap a different memory and the CPU can still communicate with it.
     *      IWashTunnel -
     *          A CPU is not tied to a particular wash tunnel, it can be react to signals of an other wash tunnel (or anything else as a matter of fact).
     *      IInvoiceBuilder -
     *          There can be different implementations of an invoice builder (PDF, Printer, Poster etc...)
     *   
     * Learn more: 
     *   https://en.wikipedia.org/wiki/Command_pattern
     */

    public class CentralProcessingUnit : 
        ISignalHandler<WashProgramSelectedSignal>,
        ISignalHandler<IndividualCustomerInfoEnteredSignal>,
        ISignalHandler<CompanyCustomerInfoEnteredSignal>,
        ISignalHandler<VehicleWashingStartedSignal>,
        ISignalHandler<VehicleAlreadyCleanSignal>,
        ISignalHandler<VehicleReadySignal>
    {
        private readonly IMemory _memory;
        private readonly IWashTunnel _washTunnel;
        private readonly IInvoiceBuilder _invoiceBuilder;

        public CentralProcessingUnit(
            IMemory memory,
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
            _memory.SetOrOverride("VWSS", signal);
            if (_memory.TryGet("WPSS", out WashProgramSelectedSignal _signal))
            {
                _washTunnel.Wash(signal.Vehicle, _signal.Program);
            }
        }

        public void Handle(VehicleReadySignal signal)
        {
            _memory.TryGet("VWSS", out VehicleWashingStartedSignal _signal);
            _signal.InvoiceCallback.Invoke(GenerateInvoiceReport());
            _memory.Flush();
        }

        public void Handle(VehicleAlreadyCleanSignal signal)
        {
            _memory.TryGet("VWSS", out VehicleWashingStartedSignal _signal);
            _signal.InvoiceCallback.Invoke("No wash step was applied since the vehicle is already clean!");
            _memory.Flush();
        }

        private string GenerateInvoiceReport()
        {
            IProgramSelector selector = null;

            if (_memory.TryGet("ICIES", out CustomerInfoEnteredSignal info))
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
