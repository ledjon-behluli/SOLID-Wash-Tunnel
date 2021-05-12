using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Materials;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class Waxing : WashStep
    {
        public override decimal Price => 2.2m;
        private readonly IWax wax;

        public Waxing(IWax wax)
        {
            this.wax = wax;
        }

        public override void Execute(IVehicle vehicle)
        {
            vehicle.ApplyWashStep(this);
            base.Execute(vehicle);
        }

        public override string Describe()
        {
            return $"Waxing using a {wax.GetType()}";
        }
    }
}
