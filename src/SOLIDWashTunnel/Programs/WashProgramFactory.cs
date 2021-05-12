using System;

namespace SOLIDWashTunnel.Programs
{
    public enum ProgramType
    {
        Fast = 1,
        Economic = 2,
        AllRounder = 3,
        Custom = 4
    }

    /* 
     * Pattern: Factory Method
     * Reason: Decouple the selection of a wash program from the Client.
     * Learn more: https://refactoring.guru/design-patterns/factory-comparison 
     */

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
                _ => throw new NotSupportedException("Specified wash program is not available!"),
            };
    }
}
