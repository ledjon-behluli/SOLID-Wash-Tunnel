using SOLIDWashTunnel.Tunnel.Steps;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs
{
    public class FastWashProgram : IWashProgram
    {
        public string Name => "Fast";

        private readonly IWashStepFactory _washStepFactory;

        public FastWashProgram(IWashStepFactory washStepFactory)
        {
            _washStepFactory = washStepFactory;
        }

        public IEnumerable<IWashStep> GetWashSteps() =>
            new List<IWashStep>()
            {
                _washStepFactory.Create(WashStepType.ChasisAndWheelWashing),
                _washStepFactory.Create(WashStepType.HighPressureWashing),
                _washStepFactory.Create(WashStepType.AirDrying)
            };
    }
}
