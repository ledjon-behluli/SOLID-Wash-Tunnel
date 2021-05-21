using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class AirDrying : WashStep
    {
        public override int CleanlinessFactor => 1;
        public override Money Price => Money.Create(0.5m);

        public override void Execute(IVehicle vehicle)
        {
            vehicle.ApplyWashStep(this);
            base.Execute(vehicle);
        }

        public override string GetDescription()
        {
            return "Air drying";
        }
    }
}
