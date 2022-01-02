
namespace SOLIDWashTunnel.Control.Signals
{
    public abstract class CustomerInfoEnteredSignal : ISignal
    {
        public Currency PreferredCurrency { get; }

        public CustomerInfoEnteredSignal(Currency preferredCurrency)
        {
            PreferredCurrency = preferredCurrency;
        }
    }
}
