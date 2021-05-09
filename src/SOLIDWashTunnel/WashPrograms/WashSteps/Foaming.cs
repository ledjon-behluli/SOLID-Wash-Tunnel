using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.WashPrograms.WashSteps
{
    public abstract class Foaming : WashStep
    {

    }

    public class ColorFoaming : Foaming
    {
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

    public class ThreeColorFoaming : Foaming
    {
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
