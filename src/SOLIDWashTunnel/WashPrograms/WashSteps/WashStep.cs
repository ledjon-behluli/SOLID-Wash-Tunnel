using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.WashPrograms.WashSteps
{
    public interface IWashStep
    {
        IWashStep NextStep(IWashStep washStep);
        void Execute(IVehicle vehicle);
        string Describe();
    }

    public abstract class WashStep : IWashStep
    {
        protected readonly IVehicle vehicle;
        private IWashStep nextStep;

        public IWashStep NextStep(IWashStep washStep)
        {
            nextStep = washStep;
            return nextStep;
        }

        public virtual void Execute(IVehicle vehicle)
        {
            if (nextStep != null)
            {
                nextStep.Execute(vehicle);
            }
        }

        public abstract string Describe();
    }
}
