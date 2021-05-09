using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.WashComponents;

namespace SOLIDWashTunnel.WashPrograms.WashSteps
{
    public class Shampooing : WashStep
    {
        private readonly IShampoo shampoo;

        public Shampooing(IShampoo shampoo)
        {
            this.shampoo = shampoo;
        }

        public override void Execute(IVehicle vehicle)
        {
            vehicle.ApplyWashStep(this);
            base.Execute(vehicle);
        }

        public override string Describe()
        {
            return $"Shampooing using a {shampoo.GetType()}";
        }
    }
}
