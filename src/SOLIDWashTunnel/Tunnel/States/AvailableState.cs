using System.Linq;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Tunnel.Steps;
using SOLIDWashTunnel.ClientFacing;
using System;
using SOLIDWashTunnel.Control;
using SOLIDWashTunnel.Control.Signals;

namespace SOLIDWashTunnel.Tunnel.States
{
    public class AvailableState : IWashTunnelState
    {
        private readonly IWashTunnel _washTunnel;
        private readonly ISignalTransmitter _transmitter;
        private readonly IWashStepNotifier _notifier;

        public AvailableState(
            IWashTunnel washTunnel,
            ISignalTransmitter transmitter,
            IWashStepNotifier notifier)
        {
            _washTunnel = washTunnel;
            _transmitter = transmitter;
            _notifier = notifier;
        }

        public void Handle(IVehicle vehicle, IWashProgram program)
        {
            if (_washTunnel.State is BusyState)
            {
                throw new Exception("Only one vehicle can be washed at a time.");
            }

            _washTunnel.State = new BusyState();

            IWashStep[] washSteps = program.GetWashSteps().ToArray();

            for (int i = 0; i < washSteps.Length - 1; i++)
            {
                washSteps[i].NextStep(washSteps[i + 1]);
            }

            washSteps[0].Act(vehicle, (action, status) => 
                _notifier.Notify(new WashStepResult(action as IWashStep, status)
            ));

            _washTunnel.State = new AvailableState(_washTunnel, _transmitter, _notifier);
            _transmitter.Transmit(new VehicleReadySignal());
        }
    }
}
