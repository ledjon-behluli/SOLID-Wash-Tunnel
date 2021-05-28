using SOLIDWashTunnel.Programs.Steps;

namespace SOLIDWashTunnel.ClientFacing
{
    public interface IWashStepSubscriber
    {
        void OnStateChange(IWashStep step);
    }
}