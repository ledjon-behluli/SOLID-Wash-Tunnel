using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Sensors;
using SOLIDWashTunnel.Control;

namespace SOLIDWashTunnel.Tunnel
{

    // TODO: Decorator pattern
    public class SmartWashTunnel : IWashTunnel
    {
        private readonly IWashTunnel _washTunnel;
        private readonly IMotherboard _motherboard;
        private readonly IDirtinessSensor _dirtinessSensor;

        public SmartWashTunnel(
            IWashTunnel washTunnel,
            IMotherboard motherboard,
            IDirtinessSensor dirtinessSensor)
        {
            _washTunnel = washTunnel;
            _motherboard = motherboard;
            _dirtinessSensor = dirtinessSensor;
        }

        public void Wash(IVehicle vehicle, IWashProgram program)
        {
            if (_dirtinessSensor.IsDirty(vehicle))
            {
                _motherboard.Transmit(new VehicleReadySignal());
                return;
            }

            _washTunnel.Wash(vehicle, program);
        }

        public void TransitionState(IWashTunnelState state)
        {
            _washTunnel.TransitionState(state);
        }
    }
}
