using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Tunnel.States;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IWashTunnel
    {
        void Wash(IVehicle vehicle, IWashProgram program);
        void TransitionState(IWashTunnelState state);
    }
}
