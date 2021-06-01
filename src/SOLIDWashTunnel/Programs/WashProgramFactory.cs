using SOLIDWashTunnel.Programs.Steps;
using System;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs
{
    /* 
    * Pattern: 
    *   Simple Factory
    *   
    * Reason: 
    *   Decouple the retrival of a wash program based on the program type.
    *   Not to be confused with Factory Method or Abstract Factory patterns.
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
        private readonly Lazy<IDictionary<ProgramType, Func<IWashStep[], IWashProgram>>> _programs;
            
        public WashProgramFactory(IDictionary<ProgramType, Func<IWashStep[], IWashProgram>> programs)
        {
            _programs = new Lazy<IDictionary<ProgramType, Func<IWashStep[], IWashProgram>>>(programs);
        }

        public IWashProgram Create(ProgramType type, params IWashStep[] washSteps)
        {
            if (!_programs.Value.TryGetValue(type, out Func<IWashStep[], IWashProgram> _func))
            {
                throw new NotSupportedException($"No wash program was found for provided type = {type}");
            }

            return _func.Invoke(washSteps);
        }
    }
}
