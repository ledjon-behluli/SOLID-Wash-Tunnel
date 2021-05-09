using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.WashComponents;

namespace SOLIDWashTunnel.WashPrograms.WashSteps
{
    public class Drying : WashStep
    {
        public override decimal Price => 0.5m;
        private readonly IDryer dryer;

        public Drying(IDryer dryer)
        {
            this.dryer = dryer;
        }

        public override void Execute(IVehicle vehicle)
        {
            vehicle.ApplyWashStep(this);
            base.Execute(vehicle);
        }

        public override string Describe()
        {
            return $"Drying using a {dryer.GetType()}";
        }
    }
}
