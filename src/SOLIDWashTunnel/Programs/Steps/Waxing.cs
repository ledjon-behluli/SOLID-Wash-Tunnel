using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class Waxing : WashStep
    {
        public override int CleanlinessFactor => 2;
        public override Money Price => Money.Create(2.2m);

        public override void Visit(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Visit(vehicle);
        }

        public override string GetDescription()
        {
            return "Waxing";
        }
    }
}
