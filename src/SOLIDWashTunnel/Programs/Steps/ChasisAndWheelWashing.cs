using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class ChasisAndWheelWashing : WashStep
    {
        public override int CleaningFactor => 3;
        public override Money Price => Money.Create(1.5m);

        public override void Act(IVehicle vehicle)
        {
            vehicle.Accept(this);
            base.Act(vehicle);
        }

        public override string GetDescription()
        {
            return "Chasis & wheels washing";
        }
    }
}
