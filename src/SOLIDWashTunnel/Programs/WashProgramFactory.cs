using System;

namespace SOLIDWashTunnel.Programs
{
    /* 
    * Pattern: 
    *   Simple Factory
    *   
    * Reason: 
    *   Decouple the selection of a wash program from the customer.
    *   
    * Learn more: 
    *   https://refactoring.guru/design-patterns/factory-comparison
    */

    public enum ProgramType
    {
        Fast = 1,
        Economic = 2,
        AllRounder = 3,
        Custom = 4
    }

    public interface IWashProgramFactory
    {
        IWashProgram Create(ProgramType type);
    }

    public class WashProgramFactory : IWashProgramFactory
    {
        public IWashProgram Create(ProgramType type) =>
            type switch
            {
                ProgramType.Fast => new FastWashProgram(),
                ProgramType.Economic => new EconomicWashProgram(),
                ProgramType.AllRounder => new AllRounderWashProgram(),
                ProgramType.Custom => new CustomWashProgram(),
                _ => throw new NotSupportedException(),
            };
    }
}
