using SOLIDWashTunnel.Programs.Steps;

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

    public enum ProgramType
    {
        Fast,
        Economic,
        AllRounder
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
