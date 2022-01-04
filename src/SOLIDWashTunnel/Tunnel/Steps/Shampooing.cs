using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class Shampooing : WashStep
    {
        public override int CleaningFactor => 1;
        public override Money Price => Money.Create(0.8m);

        public override void Act(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Act(vehicle);
        }

        public override string GetDescription()
        {
            return "Shampooing";
        }
    }
}
