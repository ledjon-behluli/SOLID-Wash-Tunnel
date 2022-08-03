using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Tunnel.States;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IWashTunnel
    {
        IWashTunnelState State { get; set; }
        void Wash(IVehicle vehicle, IWashProgram program);
    }
}
