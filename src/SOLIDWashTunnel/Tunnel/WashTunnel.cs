using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;
using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Tunnel.Steps;
using SOLIDWashTunnel.Control.Signals;
using SOLIDWashTunnel.Tunnel.States;

namespace SOLIDWashTunnel.Tunnel
{
    public class WashTunnel : IWashTunnel
    {
        private readonly ISignalTransmitter _transmitter;
        private IWashTunnelState _state;

        public WashTunnel(
            ISignalTransmitter transmitter,
            IWashStepNotifier notifier)
        {
            _transmitter = transmitter;
            _state = new AvailableState(this, notifier);
        }

        public void Wash(IVehicle vehicle, IWashProgram program)
        {
            _state.Handle(vehicle, program);
        }

        public void TransitionState(IWashTunnelState state)
        {
            _state = state;
            if (state is AvailableState)
            {
                _transmitter.Transmit(new VehicleReadySignal());
            }
        }
    }
}
