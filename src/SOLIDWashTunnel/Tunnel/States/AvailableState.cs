using System.Linq;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Tunnel.Steps;
using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Tunnel.States
{
    public class AvailableState : IWashTunnelState
    {
        private readonly IWashTunnel _washTunnel;
        private readonly IWashStepNotifier _notifier;

        public AvailableState(
            IWashTunnel washTunnel,
            IWashStepNotifier notifier)
        {
            _washTunnel = washTunnel;
            _notifier = notifier;
        }

        public void Handle(IVehicle vehicle, IWashProgram program)
        {
            _washTunnel.TransitionState(new BusyState());
            IWashStep[] washSteps = program.GetWashSteps().ToArray();

            for (int i = 0; i < washSteps.Length - 1; i++)
            {
                washSteps[i].NextStep(washSteps[i + 1]);
            }

            washSteps[0].Act(vehicle, (action, status) => 
                _notifier.Notify(new WashStepResult(action as IWashStep, status)
            ));

            _washTunnel.TransitionState(new AvailableState(_washTunnel, _notifier));
        }
    }
}
