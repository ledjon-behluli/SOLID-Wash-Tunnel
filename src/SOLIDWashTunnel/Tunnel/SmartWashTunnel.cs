using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Sensors;
using SOLIDWashTunnel.Control;
using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Control.Signals;
using SOLIDWashTunnel.Tunnel.States;

namespace SOLIDWashTunnel.Tunnel
{
    /* 
    * Pattern: 
    *   Decorator
    *
    * Reason: 
    *   Adds the ability to add new operations to existing object structures without modifying their structures.
    *   We are decorating/wrapping the WashTunnel with the SmartWashTunnel to enhance its functionality.
    *   The SmartWashTunnel is able to detect (via the dirtiness sensor) if a vehicle is clean or dirty.
    *   The dirtiness sensor is configured to detect level of dirtiness on a vehicle and if it is considered to be 'clean'
    *   the smart tunnel will completely by-pass washing the vehicle.
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Decorator_pattern
    */

    public class SmartWashTunnel : IWashTunnel
    {
        private readonly IWashTunnel _washTunnel;
        private readonly ISignalTransmitter _transmitter;
        private readonly IDirtinessSensor _sensor;

        public SmartWashTunnel(
            IWashTunnel washTunnel,
            ISignalTransmitter transmitter,
            IDirtinessSensor sensor)
        {
            _washTunnel = washTunnel;
            _transmitter = transmitter;
            _sensor = sensor;
        }

        public void Wash(IVehicle vehicle, IWashProgram program)
        {
            if (!_sensor.IsDirty(vehicle))
            {
                _transmitter.Transmit(new VehicleAlreadyCleanSignal());
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
