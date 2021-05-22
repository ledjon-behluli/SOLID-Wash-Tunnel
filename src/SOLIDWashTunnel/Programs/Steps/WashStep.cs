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

    /*
     * Principle:
     *   Interface Seggregation:
     *   
     * Reason:
     *   Clients should not be forced to implement interfaces they don't use.
     *   A vehicle does not care about information of an wash step like: price, next step to apply, or the step description.
     *   A vehicle just need to be washed a.k.a apply a wash action. That is why we have sepparated the IWashAction from IWashStep. 
     *   
     * Learn more:
     *   https://en.wikipedia.org/wiki/Interface_segregation_principle
     */

    public interface IWashStep : IWashAction
    {
        Money Price { get; }

        IWashStep NextStep(IWashStep washStep);
        string GetDescription();
    }

    public abstract class WashStep : IWashStep
    {
        public abstract int CleanlinessFactor { get; }
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
