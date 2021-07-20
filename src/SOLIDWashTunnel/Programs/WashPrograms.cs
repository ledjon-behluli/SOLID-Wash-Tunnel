using SOLIDWashTunnel.Programs.Steps;
using System;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs
{
    public interface IWashProgram
    {
        string Name { get; }
        IEnumerable<IWashStep> GetWashSteps();
    }


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
            var washSteps = new List<IWashStep>();

            foreach (WashStepType type in Enum.GetValues(typeof(WashStepType)))
            {
                washSteps.Add(_washStepFactory.Create(type));
            }

            return washSteps;
        }
    }

    public class CustomWashProgram : IWashProgram
    {
        public string Name => "Custom";

        private IEnumerable<IWashStep> _washSteps;

        public CustomWashProgram(IEnumerable<IWashStep> washSteps)
        {
            _washSteps = washSteps;
        }

        public IEnumerable<IWashStep> GetWashSteps() => _washSteps;
    }
}
