using System;

namespace SOLIDWashTunnel.WashPrograms
{
    public enum ProgramType
    {
        Fast = 1,
        Economic = 2,
        AllRounder = 3,
        Custom = 4
    }

    /* 
     * Pattern: Simple factory pattern
     * Reason: Decouple the selection of a wash program from the Client.
     * Learn more: https://refactoring.guru/design-patterns/factory-comparison 
     */

    public class WashProgramFactory
    {
        public static IWashProgram GetProgram(ProgramType type) =>
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
