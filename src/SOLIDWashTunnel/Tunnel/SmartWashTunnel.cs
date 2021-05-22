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
        private readonly IDirtinessSensor _sensor;

        public SmartWashTunnel(
            IWashTunnel washTunnel,
            IMotherboard motherboard,
            IDirtinessSensor sensor)
        {
            _washTunnel = washTunnel;
            _motherboard = motherboard;
            _sensor = sensor;
        }

        public void Wash(IVehicle vehicle, IWashProgram program)
        {
            if (!_sensor.IsDirty(vehicle))
            {
                _motherboard.Transmit(new VehicleAlreadyCleanSignal());
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
