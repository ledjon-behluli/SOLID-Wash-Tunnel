using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class Shampooing : WashStep
    {
        public override int CleanlinessFactor => 1;
        public override Money Price => Money.Create(0.8m);

        public override void Visit(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Visit(vehicle);
        }

        public override string GetDescription()
        {
            return "Shampooing";
        }
    }
}
