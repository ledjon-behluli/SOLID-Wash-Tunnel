using SOLIDWashTunnel.Tunnel.Steps;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs
{
    public class EconomicWashProgram : IWashProgram
    {
        public string Name => "Economic";

        private readonly IWashStepFactory _washStepFactory;

        public EconomicWashProgram(IWashStepFactory washStepFactory)
        {
            _washStepFactory = washStepFactory;
        }

        public IEnumerable<IWashStep> GetWashSteps() =>
            new List<IWashStep>()
            {
                _washStepFactory.Create(WashStepType.ChasisAndWheelWashing),
                _washStepFactory.Create(WashStepType.Shampooing),
                _washStepFactory.Create(WashStepType.HighPressureWashing),
                _washStepFactory.Create(WashStepType.SingleColorFoaming),
                _washStepFactory.Create(WashStepType.HighPressureWashing),
                _washStepFactory.Create(WashStepType.AirDrying)
            };
    }
}
