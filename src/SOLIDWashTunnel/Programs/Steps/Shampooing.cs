using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Materials;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class Shampooing : WashStep
    {
        public override decimal Price => 0.8m;
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
