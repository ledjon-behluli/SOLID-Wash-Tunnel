using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class SingleColorFoaming : WashStep
    {
        public override int CleaningFactor => 1;
        public override Money Price => Money.Create(1.1m);

        public override void Visit(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Visit(vehicle);
        }

        public override string GetDescription()
        {
            return "Foaming using a single color foam";
        }
    }

    public class ThreeColorFoaming : WashStep
    {
        public override int CleaningFactor => 2;
        public override Money Price => Money.Create(1.7m);

        public override void Visit(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Visit(vehicle);
        }

        public override string GetDescription()
        {
            return "Foaming using a three color foam";
        }
    }
}
