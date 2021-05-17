using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IWashTunnel
    {
        void Wash(IVehicle vehicle, IWashProgram program);
        void TransitionState(IWashTunnelState state);
    }

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
