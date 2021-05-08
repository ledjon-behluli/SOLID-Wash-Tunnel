using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDWashTunnel.WashPrograms
{
    /* 
     * Pattern: Simple factory pattern
     * Reason: Decouple the selection of a wash program from the Client.
     * Learn more: https://refactoring.guru/design-patterns/factory-comparison 
     */
     
    public class WashProgramFactory
    {
        public static IWashProgram GetProgram(int programId) =>
            programId switch
            {
                1 => new FastWashProgram(),
                2 => new EconomicWashProgram(),
                3 => new AllRounderWashProgram(),
                4 => new CustomWashProgram(),
                _ => throw new NotSupportedException("Specified wash program is not available!"),
            };
    }
}
