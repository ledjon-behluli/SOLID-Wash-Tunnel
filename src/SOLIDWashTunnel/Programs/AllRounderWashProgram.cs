using SOLIDWashTunnel.Tunnel.Steps;
using System;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs
{
    public class AllRounderWashProgram : IWashProgram
    {
        public string Name => "All rounder";

        private readonly IWashStepFactory _washStepFactory;

        public AllRounderWashProgram(IWashStepFactory washStepFactory)
        {
            _washStepFactory = washStepFactory;
        }

        public IEnumerable<IWashStep> GetWashSteps()
        {
            foreach (WashStepType type in Enum.GetValues(typeof(WashStepType)))
            {
                yield return _washStepFactory.Create(type);
            }
        }
    }
}
