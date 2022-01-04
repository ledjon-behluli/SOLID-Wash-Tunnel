using SOLIDWashTunnel.Tunnel.Steps;
using System;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs
{
    /* 
    * Pattern: 
    *   Simple Factory
    *   
    * Reason: 
    *   Create a wash program based on the program type.
    *   
    * Learn more: 
    *   https://refactoring.guru/design-patterns/factory-comparison
    */

    public interface IWashProgramFactory
    {
        IWashProgram Create(ProgramType type, params IWashStep[] washSteps);
    }

    public class WashProgramFactory : IWashProgramFactory
    {
        private readonly Lazy<IDictionary<ProgramType, Func<IWashStep[], IWashProgram>>> _programsMap;
            
        public WashProgramFactory(IDictionary<ProgramType, Func<IWashStep[], IWashProgram>> programs)
        {
            _programsMap = new Lazy<IDictionary<ProgramType, Func<IWashStep[], IWashProgram>>>(programs);
        }

        public IWashProgram Create(ProgramType type, params IWashStep[] washSteps)
        {
            if (!_programsMap.Value.TryGetValue(type, out Func<IWashStep[], IWashProgram> _func))
            {
                throw new NotSupportedException($"Wash program type {type} is not supported!");
            }

            return _func.Invoke(washSteps);
        }
    }
}
