using SOLIDWashTunnel.Programs.Steps;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs
{
    public interface IWashProgram
    {
        int Id { get; }
        string Name { get; }
        IEnumerable<IWashStep> GetWashSteps();
    }

    public class FastWashProgram : IWashProgram
    {
        public int Id => 1;
        public string Name => "Fast";

        public IEnumerable<IWashStep> GetWashSteps() =>
            new List<IWashStep>()
            {
                new ChasisAndWheelWashing(),
                new HighPressureWashing(),
                new AirDrying()
            };
    }

    public class EconomicWashProgram : IWashProgram
    {
        public int Id => 2;
        public string Name => "Economic";

        public IEnumerable<IWashStep> GetWashSteps() =>
            new List<IWashStep>()
            {
                new ChasisAndWheelWashing(),
                new Shampooing(),
                new HighPressureWashing(),
                new SingleColorFoaming(),
                new HighPressureWashing(),
                new AirDrying()
            };
    }

    public class AllRounderWashProgram : IWashProgram
    {
        public int Id => 3;
        public string Name => "All rounder";

        public IEnumerable<IWashStep> GetWashSteps() =>
            new List<IWashStep>()
            {
                new ChasisAndWheelWashing(),
                new Shampooing(),
                new HighPressureWashing(),
                new ThreeColorFoaming(),
                new HighPressureWashing(),
                new AirDrying(),
                new Waxing()
            };
    }

    public class CustomWashProgram : IWashProgram
    {
        public int Id => 4;
        public string Name => "Custom";

        private IEnumerable<IWashStep> _washSteps;

        public CustomWashProgram(IEnumerable<IWashStep> washSteps)
        {
            _washSteps = washSteps;
        }

        public IEnumerable<IWashStep> GetWashSteps() => _washSteps;
    }
}
