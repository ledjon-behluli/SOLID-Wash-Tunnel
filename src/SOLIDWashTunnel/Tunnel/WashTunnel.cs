using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;
using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Tunnel.Steps;
using SOLIDWashTunnel.Tunnel.States;

namespace SOLIDWashTunnel.Tunnel
{
    public class WashTunnel : IWashTunnel
    {
        public IWashTunnelState State { get; set; }

        public WashTunnel(
            ISignalTransmitter transmitter,
            IWashStepNotifier notifier)
        {
            State = new AvailableState(this, transmitter, notifier);
        }

        public void Wash(IVehicle vehicle, IWashProgram program)
        {
            State.Handle(vehicle, program);
        }
    }
}
