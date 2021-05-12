using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class ColorFoaming : WashStep
    {
        public override decimal Price => 1.1m;

        public override string Describe()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(IVehicle vehicle)
        {
            vehicle.ApplyWashStep(this);
            base.Execute(vehicle);
        }
    }

    public class ThreeColorFoaming : WashStep
    {
        public override decimal Price => 1.7m;

        public override string Describe()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(IVehicle vehicle)
        {
            vehicle.ApplyWashStep(this);
            base.Execute(vehicle);
        }
    }
}
