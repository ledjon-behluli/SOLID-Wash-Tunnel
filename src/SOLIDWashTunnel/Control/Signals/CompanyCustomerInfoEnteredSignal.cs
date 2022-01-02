
namespace SOLIDWashTunnel.Control.Signals
{
    public class CompanyCustomerInfoEnteredSignal : CustomerInfoEnteredSignal
    {
        public string CompanyName { get; }

        public CompanyCustomerInfoEnteredSignal(string companyName, Currency preferredCurrency)
            : base(preferredCurrency)
        {
            CompanyName = companyName;
        }

        private class CompanyCustomerInfoEnteredSignalHandler : ISignalHandler<CompanyCustomerInfoEnteredSignal>
        {
            private readonly IMemory _memory;

            public CompanyCustomerInfoEnteredSignalHandler(IMemory memory)
            {
                _memory = memory;
            }

            public void Handle(CompanyCustomerInfoEnteredSignal signal)
                => _memory.SetOrOverride("CCIES", signal);
        }
    }
}
