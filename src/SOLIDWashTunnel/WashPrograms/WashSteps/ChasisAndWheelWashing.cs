using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.WashComponents;

namespace SOLIDWashTunnel.WashPrograms.WashSteps
{
    public class ChasisAndWheelWashing : WashStep
    {
        private readonly IBrush brush;

        public ChasisAndWheelWashing(IBrush brush)
        {
            this.brush = brush;
        }

        public override void Execute(IVehicle vehicle)
        {
            vehicle.ApplyWashStep(this);
            base.Execute(vehicle);
        }

        public override string Describe()
        {
            return $"Washing Chasis & Wheels using a {brush.GetType()}";
        }
    }
}
