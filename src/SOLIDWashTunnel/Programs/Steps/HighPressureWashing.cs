using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class HighPressureWashing : WashStep
    {
        public override int CleanlinessFactor => 5;
        public override Money Price => Money.Create(0.3m);

        public override void Execute(IVehicle vehicle)
        {
            vehicle.ApplyWashStep(this);
            base.Execute(vehicle);
        }

        public override string GetDescription()
        {
            return "High water pressure washing";
        }
    }
}
