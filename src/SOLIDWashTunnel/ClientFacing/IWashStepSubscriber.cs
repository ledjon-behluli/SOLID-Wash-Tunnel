using SOLIDWashTunnel.Tunnel.Steps;

namespace SOLIDWashTunnel.ClientFacing
{
    public interface IWashStepSubscriber
    {
        void OnStepApplied(IWashStep step);
        void OnStepSkipped(IWashStep step);
    }
}