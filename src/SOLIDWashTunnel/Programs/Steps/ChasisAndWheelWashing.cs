using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class ChasisAndWheelWashing : WashStep
    {
        public override int CleanlinessFactor => 3;
        public override Money Price => Money.Create(1.5m);

        public override void Visit(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Visit(vehicle);
        }

        public override string GetDescription()
        {
            return "Chasis & wheels washing";
        }
    }
}
