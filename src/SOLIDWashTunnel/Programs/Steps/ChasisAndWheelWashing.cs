using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Materials;

namespace SOLIDWashTunnel.Programs.Steps
{
    public class ChasisAndWheelWashing : WashStep
    {
        public override decimal Price => 1.5m;
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
