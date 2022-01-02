using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class HighPressureWashing : WashStep
    {
        public override int CleaningFactor => 5;
        public override Money Price => Money.Create(0.3m);

        public override void Act(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Act(vehicle);
        }

        public override string GetDescription()
        {
            return "High water pressure washing";
        }
    }
}
