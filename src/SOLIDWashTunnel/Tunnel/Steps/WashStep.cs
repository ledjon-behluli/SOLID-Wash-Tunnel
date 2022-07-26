using SOLIDWashTunnel.ClientFacing;
using System;

namespace SOLIDWashTunnel.Tunnel.Steps
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
     *   A vehicle does not care about information of a wash step like: price, next step to apply, or step-description.
     *   A vehicle just needs to be washed a.k.a apply a wash action. That is why we have sepparated the IWashAction from IWashStep. 
     *   
     * Learn more:
     *   https://en.wikipedia.org/wiki/Interface_segregation_principle
     */

    public interface IWashStep : IWashAction
    {
        int Id { get; }
        Money Price { get; }

        IWashStep NextStep(IWashStep washStep);
        string GetDescription();
    }

    public abstract class WashStep : IWashStep
    {
        public abstract int CleaningFactor { get; }
        public abstract int Id { get; }
        public abstract Money Price { get; }

        private IWashStep nextStep;

        public IWashStep NextStep(IWashStep washStep)
        {
            nextStep = washStep;
            return nextStep;
        }

        public virtual void Act(IVehicle vehicle, Action<IWashAction, bool> callback)
        {
            if (nextStep != null)
            {
                nextStep.Act(vehicle, callback);
            }
        }

        public abstract string GetDescription();
    }
}
