using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class HighPressureWashing : WashStep
    {
        public override int CleaningFactor => 5;
        public override Money Price => Money.Create(0.3m);

        public override void Visit(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Visit(vehicle);
        }

        public override string GetDescription()
        {
            return "High water pressure washing";
        }
    }
}
