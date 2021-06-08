namespace SOLIDWashTunnel.Control.Signals
{
    public class VehicleAlreadyCleanSignal : ISignal
    {
        private class VehicleAlreadyCleanSignalHandler : ISignalHandler<VehicleAlreadyCleanSignal>
        {
            private readonly IMemory _memory;

            public VehicleAlreadyCleanSignalHandler(IMemory memory)
            {
                _memory = memory;
            }

            public void Handle(VehicleAlreadyCleanSignal signal)
            {
                _memory.TryGet("VWSS", out VehicleWashingStartedSignal _signal);
                _signal.InvoiceCallback.Invoke("No wash step was applied since the vehicle is already clean!");
                _memory.Flush();
            }
        }
    }
}
