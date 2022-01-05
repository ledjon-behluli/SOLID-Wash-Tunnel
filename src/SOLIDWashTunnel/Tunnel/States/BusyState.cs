using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Tunnel.States
{
    public class BusyState : IWashTunnelState
    {
        public void Handle(IVehicle vehicle, IWashProgram program)
        {
            /* 
             * The wash tunnel can't accept washing a new vehicle, while an other vehicle is being washed from the tunnel.
             * So we simply ignore this request. We could extend this to queue a new washing process and start it when
             * the previous vehicle is ready.
             */
        }
    }
}
