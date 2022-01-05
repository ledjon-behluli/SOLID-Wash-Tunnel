using SOLIDWashTunnel.Tunnel.Steps;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs
{
    public interface IWashProgram
    {
        string Name { get; }
        IEnumerable<IWashStep> GetWashSteps();
    }
}
