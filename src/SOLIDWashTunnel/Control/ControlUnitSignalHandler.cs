using SOLIDWashTunnel.Tunnel;

namespace SOLIDWashTunnel.Control
{
    public class ControlUnitSignalHandler : 
        IControlUnitSignalHandler<WashProgramSelectedSignal>,
        IControlUnitSignalHandler<IndividualCustomerInfoEnteredSignal>,
        IControlUnitSignalHandler<CompanyCustomerInfoEnteredSignal>,
        IControlUnitSignalHandler<VehicleWashingStartedSignal>,
        IControlUnitSignalHandler<VehicleReadySignal>
    {
        private readonly IControlUnitMemory _memory;
        private readonly IWashTunnel _washTunnel;

        public ControlUnitSignalHandler(
            IControlUnitMemory memory,
            IWashTunnel washTunnel)
        {
            _memory = memory;
            _washTunnel = washTunnel;
        }


        public void Handle(WashProgramSelectedSignal signal)
            => _memory.Set("WPSS", signal);
      
        public void Handle(IndividualCustomerInfoEnteredSignal signal)
            => _memory.Set("ICIES", signal);

        public void Handle(CompanyCustomerInfoEnteredSignal signal)
            => _memory.Set("CCIES", signal);

        public void Handle(VehicleWashingStartedSignal signal)
        {
            var _signal = _memory.Get<WashProgramSelectedSignal>("WPSS");
            _washTunnel.Wash(signal.Vehicle, _signal.Program);
        }

        public void Handle(VehicleReadySignal signal)
        {
            // TODO: use invoice builder;
        }
    }
}
