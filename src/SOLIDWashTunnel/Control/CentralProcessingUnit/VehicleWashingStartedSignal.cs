using SOLIDWashTunnel.ClientFacing;
using SOLIDWashTunnel.Tunnel;
using System;

namespace SOLIDWashTunnel.Control.Signals
{
    public class VehicleWashingStartedSignal : ISignal
    {
        public IVehicle Vehicle { get; }
        public Action<string> InvoiceCallback { get; }

        public VehicleWashingStartedSignal(IVehicle vehicle, Action<string> invoiceCallback)
        {
            Vehicle = vehicle;
            InvoiceCallback = invoiceCallback;
        }

        private class VehicleWashingStartedSignalHandler : ISignalHandler<VehicleWashingStartedSignal>
        {
            private readonly IMemory _memory;
            private readonly IWashTunnel _washTunnel;

            public VehicleWashingStartedSignalHandler(
                IMemory memory,
                IWashTunnel washTunnel)
            {
                _memory = memory;
                _washTunnel = washTunnel;
            }

            public void Handle(VehicleWashingStartedSignal signal)
            {
                _memory.SetOrOverride("VWSS", signal);
                if (_memory.TryGet("WPSS", out WashProgramSelectedSignal _signal))
                {
                    _washTunnel.Wash(signal.Vehicle, _signal.Program);
                }
            }
        }
    }
}
