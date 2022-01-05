using SOLIDWashTunnel.Tunnel.Steps;

namespace SOLIDWashTunnel.Invoices
{
    public interface IWashStepTracker
    {
        bool HasStepBeenApplied(IWashStep washStep);
    }
}
