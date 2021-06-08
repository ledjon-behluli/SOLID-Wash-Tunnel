using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel.Control.Signals
{
    public class WashProgramSelectedSignal : ISignal
    {
        public IWashProgram Program { get; }

        public WashProgramSelectedSignal(IWashProgram program)
        {
            Program = program;
        }

        private class WashProgramSelectedSignalHandler : ISignalHandler<WashProgramSelectedSignal>
        {
            private readonly IMemory _memory;

            public WashProgramSelectedSignalHandler(IMemory memory)
            { 
                _memory = memory;
            }

            public void Handle(WashProgramSelectedSignal signal)
                => _memory.SetOrOverride("WPSS", signal);
        }
    }
}
