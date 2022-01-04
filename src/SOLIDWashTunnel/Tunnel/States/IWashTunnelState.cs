using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Tunnel.States
{
    /* 
     * Pattern: 
     *   State
     *   
     * Reason: 
     *   Allow the wash tunnel to alter its behavior, when its internal state changes. 
     *   
     * Learn more: 
     *   https://en.wikipedia.org/wiki/State_pattern
     */

    public interface IWashTunnelState
    {
        void Handle(IVehicle vehicle, IWashProgram program);
    }
}
