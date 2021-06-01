using SOLIDWashTunnel.Programs.Steps;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs
{
    /* 
     * Pattern:
     *   Builder
     *   
     * Reason: 
     *   Give the client a convenient way to build the wash program, through a series of build steps.
     *   
     * Learn more: 
     *   https://en.wikipedia.org/wiki/Builder_pattern
     *   https://en.wikipedia.org/wiki/Fluent_interface
     */

    public interface ICustomWashProgramBuilder
    {
        ICustomWashProgramBuilder AddChasisAndWheelWashing();
        ICustomWashProgramBuilder AddShampooing();
        ICustomWashProgramBuilder AddHighPressureWashing();
        ICustomWashProgramBuilder AddSingleColorFoaming();
        ICustomWashProgramBuilder AddThreeColorFoaming();
        ICustomWashProgramBuilder AddWaxing();
        ICustomWashProgramBuilder AddAirDrying();

        IWashProgram Build();
    }


    public class CustomWashProgramBuilder : ICustomWashProgramBuilder
    {
        private readonly List<IWashStep> _washSteps;
        private readonly IWashProgramFactory _programFactory;

        public CustomWashProgramBuilder(IWashProgramFactory programFactory)
        {
            _washSteps = new List<IWashStep>();
            _programFactory = programFactory;
        }

        #region Wash Steps

        public ICustomWashProgramBuilder AddAirDrying()
        {
            _washSteps.Add(new AirDrying());
            return this;
        }

        public ICustomWashProgramBuilder AddChasisAndWheelWashing()
        {
            _washSteps.Add(new ChasisAndWheelWashing());
            return this;
        }

        public ICustomWashProgramBuilder AddHighPressureWashing()
        {
            _washSteps.Add(new HighPressureWashing());
            return this;
        }

        public ICustomWashProgramBuilder AddShampooing()
        {
            _washSteps.Add(new Shampooing());
            return this;
        }

        public ICustomWashProgramBuilder AddSingleColorFoaming()
        {
            _washSteps.Add(new SingleColorFoaming());
            return this;
        }

        public ICustomWashProgramBuilder AddThreeColorFoaming()
        {
            _washSteps.Add(new ThreeColorFoaming());
            return this;
        }

        public ICustomWashProgramBuilder AddWaxing()
        {
            _washSteps.Add(new Waxing());
            return this;
        }

        #endregion

        public IWashProgram Build() => _programFactory.Create(ProgramType.Custom, _washSteps.ToArray());
    }
}
