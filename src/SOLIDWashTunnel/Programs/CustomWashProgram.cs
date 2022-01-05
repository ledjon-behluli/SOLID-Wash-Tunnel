using SOLIDWashTunnel.Tunnel.Steps;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs
{
    public class CustomWashProgram : IWashProgram
    {
        public string Name => "Custom";

        private readonly IEnumerable<IWashStep> _washSteps;

        public CustomWashProgram(IEnumerable<IWashStep> washSteps)
        {
            _washSteps = washSteps;
        }

        public IEnumerable<IWashStep> GetWashSteps() => _washSteps;
    }
}
