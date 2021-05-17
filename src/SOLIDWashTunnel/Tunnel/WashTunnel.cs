using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Programs.Steps;
using System.Linq;
using SOLIDWashTunnel.Control;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IWashTunnel
    {
        void Wash(IVehicle vehicle, IWashProgram program);
    }

    public class WashTunnel : IWashTunnel
    {
        private readonly IMotherboard _motherboard;

        public WashTunnel(IMotherboard motherboard)
        {
            _motherboard = motherboard;
        }

        public void Wash(IVehicle vehicle, IWashProgram program)
        {
            IWashStep[] washSteps = program.GetWashSteps().ToArray();

            for (int i = 0; i < washSteps.Length - 1; i++)
            {
                washSteps[i].NextStep(washSteps[i + 1]);
            }

            washSteps[0].Execute(vehicle);
            _motherboard.Transmit(new VehicleReadySignal());
        }
    }
}
