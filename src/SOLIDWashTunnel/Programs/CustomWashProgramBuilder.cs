using SOLIDWashTunnel.Programs.Steps;
using System;
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
        ICustomWashProgramBuilder Add(WashStepType type);
        ICustomWashProgramBuilder Add(IWashStep washStep);
        ICustomWashProgramBuilder AddAll();

        IWashProgram Build();
    }


    public class CustomWashProgramBuilder : ICustomWashProgramBuilder
    {
        private readonly List<IWashStep> _washSteps;
        private readonly IWashProgramFactory _programFactory;
        private readonly IWashStepFactory _washStepFactory;

        public CustomWashProgramBuilder(
            IWashProgramFactory programFactory,
            IWashStepFactory washStepFactory)
        {
            _washSteps = new List<IWashStep>();
            _programFactory = programFactory;
            _washStepFactory = washStepFactory;
        }


        public ICustomWashProgramBuilder Add(WashStepType type)
        {
            _washSteps.Add(_washStepFactory.Create(type));
            return this;
        }

        public ICustomWashProgramBuilder Add(IWashStep washStep)
        {
            _washSteps.Add(washStep);
            return this;
        }

        public ICustomWashProgramBuilder AddAll()
        {
            foreach (WashStepType type in (WashStepType[])Enum.GetValues(typeof(WashStepType)))
            {
                _washSteps.Add(_washStepFactory.Create(type));
            }

            return this;
        }


        public IWashProgram Build()
        {
            IWashProgram program = _programFactory.Create(ProgramType.Custom, _washSteps.ToArray());
            _washSteps.Clear();

            return program;
        }
    }
}
