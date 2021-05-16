using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Programs.Steps
{
    /* 
    * Pattern: 
    *   Chain of Responsibility
    *
    * Reason: 
    *   Pass requests along a chain of handlers. Upon receiving a request, each handler decides either to process the request 
    *   or to pass it to the next handler in the chain.
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern
    */
    public interface IWashStep
    {
        Money Price { get; }
        IWashStep NextStep(IWashStep washStep);
        void Execute(IVehicle vehicle);
        string GetDescription();
    }

    public abstract class WashStep : IWashStep
    {
        public abstract Money Price { get; }

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

        public abstract string GetDescription();
    }
}
