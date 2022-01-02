using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class Waxing : WashStep
    {
        public override int CleaningFactor => 2;
        public override Money Price => Money.Create(2.2m);

        public override void Act(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Act(vehicle);
        }

        public override string GetDescription()
        {
            return "Waxing";
        }
    }
}
