using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IWashTunnel
    {
        void Wash(IVehicle vehicle, IWashProgram program);
        void TransitionState(IWashTunnelState state);
    }
}
