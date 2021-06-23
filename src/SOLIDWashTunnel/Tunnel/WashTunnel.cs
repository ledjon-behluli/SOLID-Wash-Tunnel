using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;
using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Programs.Steps;
using SOLIDWashTunnel.Control.Signals;

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
            _state = new FreeState(this, notifier);
        }

        public void Wash(IVehicle vehicle, IWashProgram program)
        {
            _state.Handle(vehicle, program);
        }

        public void TransitionState(IWashTunnelState state)
        {
            _state = state;
            if (state is FreeState)
            {
                _transmitter.Transmit(new VehicleReadySignal());
            }
        }
    }
}
