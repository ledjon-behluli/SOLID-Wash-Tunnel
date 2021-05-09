using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.WashPrograms;
using SOLIDWashTunnel.WashPrograms.WashSteps;
using System.Linq;

namespace SOLIDWashTunnel.Tunnels
{
    public class ConveyorTunnel : IWashTunnel
    {
        private IWashProgram program;

        public ConveyorTunnel(IWashProgram program)
        {
            this.program = program;
        }

        public void SelectProgram(IWashProgram program)
        {
            this.program = program;
        }

        public void Wash(IVehicle vehicle)
        {
            IWashStep[] washSteps = program.GetWashSteps().ToArray();

            for (int i = 0; i < washSteps.Length - 1; i++)
            {
                washSteps[i].NextStep(washSteps[i + 1]);
            }

            washSteps[0].Execute(vehicle);
        }
    }
}
