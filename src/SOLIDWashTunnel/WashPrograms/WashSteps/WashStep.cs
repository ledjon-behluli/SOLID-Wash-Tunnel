using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.WashPrograms.WashSteps
{
    /* 
    * Pattern: Chain of Responsebility
    * Reason: TODO
    * Learn more: https://refactoring.guru/design-patterns/strategy
    */
    public interface IWashStep
    {
        decimal Price { get; }
        IWashStep NextStep(IWashStep washStep);
        void Execute(IVehicle vehicle);
        string Describe();
    }

    public abstract class WashStep : IWashStep
    {
        public abstract decimal Price { get; }
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
