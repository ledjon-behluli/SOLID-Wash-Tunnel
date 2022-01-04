using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class SingleColorFoaming : WashStep
    {
        public override int CleaningFactor => 1;
        public override Money Price => Money.Create(1.1m);

        public override void Act(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Act(vehicle);
        }

        public override string GetDescription()
        {
            return "Foaming using a single color foam";
        }
    }
}
