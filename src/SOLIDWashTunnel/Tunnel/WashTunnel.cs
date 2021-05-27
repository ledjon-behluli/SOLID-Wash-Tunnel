using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;
using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Tunnel
{
    public class WashTunnel : IWashTunnel
    {
        private readonly IMotherboard _motherboard;
        private IWashTunnelState _state;

        public WashTunnel(IMotherboard motherboard)
        {
            _motherboard = motherboard;
            _state = new FreeState(this);
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
                _motherboard.Transmit(new VehicleReadySignal());
            }
        }
    }
}
