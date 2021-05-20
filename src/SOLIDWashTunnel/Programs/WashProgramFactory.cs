using SOLIDWashTunnel.Programs.Steps;

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
        Fast = 0,
        Economic = 1,
        AllRounder = 2
    }

    public interface IWashProgramFactory
    {
        IWashProgram Create(ProgramType? type, params IWashStep[] washSteps);
    }

    public class WashProgramFactory : IWashProgramFactory
    {
        public IWashProgram Create(ProgramType? type, params IWashStep[] washSteps) =>
            type switch
            {
                ProgramType.Fast => new FastWashProgram(),
                ProgramType.Economic => new EconomicWashProgram(),
                ProgramType.AllRounder => new AllRounderWashProgram(),
                _ => new CustomWashProgram(washSteps)
            };
    }
}
