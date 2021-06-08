
namespace SOLIDWashTunnel.Control.Signals
{
    public class IndividualCustomerInfoEnteredSignal : CustomerInfoEnteredSignal
    {
        public string FirstName { get; }
        public string LastName { get; }

        public IndividualCustomerInfoEnteredSignal(string firstName, string lastName, Currency preferredCurrency)
            : base(preferredCurrency)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private class IndividualCustomerInfoEnteredSignalHandler : ISignalHandler<IndividualCustomerInfoEnteredSignal>
        {
            private readonly IMemory _memory;

            public IndividualCustomerInfoEnteredSignalHandler(IMemory memory)
            {
                _memory = memory;
            }

            public void Handle(IndividualCustomerInfoEnteredSignal signal)
                => _memory.SetOrOverride("ICIES", signal);
        }
    }
}
