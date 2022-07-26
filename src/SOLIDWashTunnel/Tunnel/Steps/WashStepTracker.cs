using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Invoices;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class WashStepTracker : IWashStepTracker, IWashStepSubscriber
    {
        private readonly Dictionary<int, bool> _stepIdMap = new Dictionary<int, bool>();

        public bool HasStepBeenApplied(IWashStep washStep)
        {
            if (_stepIdMap.TryGetValue(washStep.Id, out bool applied))
            {
                return applied;
            }

            return false;
        }

        public void OnStepApplied(IWashStep washStep)
        {
            _stepIdMap.TryAdd(washStep.Id, true);
        }

        public void OnStepSkipped(IWashStep washStep)
        {
            _stepIdMap.TryAdd(washStep.Id, false);
        }
    }
}
