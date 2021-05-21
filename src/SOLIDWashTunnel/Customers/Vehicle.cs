using SOLIDWashTunnel.Programs.Steps;
using SOLIDWashTunnel.Tunnel;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Vehicles
{
    public interface IVehicle
    {
        void ApplyWashStep(WashStep step);
        IReadOnlyCollection<WashStep> GetAppliedWashSteps();
    }


    public abstract class Vehicle : IVehicle
    {
        private List<WashStep> appliedWashSteps;

        public Vehicle()
        {
            appliedWashSteps = new List<WashStep>();
        }

        public void ApplyWashStep(WashStep step)
        {
            appliedWashSteps.Add(step);
        }

        public IReadOnlyCollection<WashStep> GetAppliedWashSteps()
        {
            return appliedWashSteps.AsReadOnly();
        }
    }

    public interface ICustomer
    {
        IUserPanel UserPanel { get; set; }
    }

    
}
