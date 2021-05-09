using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.WashPrograms.WashSteps
{
    public class HighPressureWashing : WashStep
    {
        public override void Execute(IVehicle vehicle)
        {
            vehicle.ApplyWashStep(this);
            base.Execute(vehicle);
        }

        public override string Describe()
        {
            return "Applying high pressure water";
        }
    }
}
