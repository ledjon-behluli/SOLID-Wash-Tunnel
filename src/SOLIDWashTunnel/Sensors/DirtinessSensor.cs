using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Sensors
{
    public interface IDirtinessSensor
    {
        IDirtinessSensor Calibrate(int threshold);
        bool IsDirty(IVehicle vehicle);
    }

    public class DirtinessSensor : IDirtinessSensor
    {
        private int _threshold = 3;

        public IDirtinessSensor Calibrate(int threshold)
        {
            _threshold = threshold;
            return this;
        }

        public bool IsDirty(IVehicle vehicle)
        {
            return vehicle.Cleanliness < _threshold;
        }
    }
}
