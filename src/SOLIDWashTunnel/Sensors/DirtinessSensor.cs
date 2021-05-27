using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Sensors
{
    public interface IDirtinessSensor
    {
        /// <inheritdoc />
        /// <summary>
        /// Calibrate this sensor to react on a certain level of dirtiness
        /// </summary>
        /// <param name="threshold">The level above which the vehicle is considered 'dirty'.</param>
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
            return vehicle.Dirtiness > _threshold;
        }
    }
}
