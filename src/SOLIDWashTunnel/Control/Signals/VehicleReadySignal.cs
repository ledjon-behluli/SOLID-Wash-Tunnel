using SOLIDWashTunnel.Invoices;
using System;

namespace SOLIDWashTunnel.Control.Signals
{
    public class VehicleReadySignal : ISignal
    {
        private class VehicleReadySignalHandler : ISignalHandler<VehicleReadySignal>
        {
            private readonly IMemory _memory;
            private readonly IInvoiceBuilder _invoiceBuilder;

            public VehicleReadySignalHandler(
                IMemory memory,
                IInvoiceBuilder invoiceBuilder)
            {
                _memory = memory;
                _invoiceBuilder = invoiceBuilder;
            }

            public void Handle(VehicleReadySignal signal)
            {
                _memory.TryGet("VWSS", out VehicleWashingStartedSignal _signal);
                _signal.InvoiceCallback.Invoke(GenerateInvoiceReport());
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

                throw new InvalidOperationException("Can not generate invoice because some critical informations about the wash session are missing.");
            }
        }
    }
}
