using SOLIDWashTunnel.WashPrograms.WashSteps;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Vehicles
{
    public interface IVehicle
    {
        IReadOnlyCollection<WashStep> AppliedWashSteps { get; }
        void ApplyWashStep(WashStep step);
    }

    public abstract class Vehicle : IVehicle
    {
        private List<WashStep> appliedWashSteps;
        public IReadOnlyCollection<WashStep> AppliedWashSteps => appliedWashSteps.AsReadOnly();

        public Vehicle()
        {
            appliedWashSteps = new List<WashStep>();
        }

        public void ApplyWashStep(WashStep step)
        {
            appliedWashSteps.Add(step);
        }
    }
}
