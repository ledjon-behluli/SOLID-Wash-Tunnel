using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.WashComponents;

namespace SOLIDWashTunnel.WashPrograms.WashSteps
{
    public class Waxing : WashStep
    {
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
