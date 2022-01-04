using SOLIDWashTunnel.Tunnel.Steps;

namespace SOLIDWashTunnel.ClientFacing
{
    public interface IWashStepSubscriber
    {
        void OnNewStepApplied(IWashStep step);
    }
}