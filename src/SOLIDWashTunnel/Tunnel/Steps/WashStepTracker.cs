using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Invoices;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Tunnel.Steps
{
    public class WashStepTracker : IWashStepTracker, IWashStepSubscriber
    {
        private readonly Dictionary<IWashStep, bool> _map = new Dictionary<IWashStep, bool>();

        public bool HasStepBeenApplied(IWashStep washStep)
        {
            if (_map.TryGetValue(washStep, out bool applied))
            {
                return applied;
            }

            return false;
        }

        public void OnStepApplied(IWashStep step)
        {
            _map.TryAdd(step, true);
        }

        public void OnStepSkipped(IWashStep step)
        {
            _map.TryAdd(step, false);
        }
    }
}
