using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Programs.Steps;
using System.Linq;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IWashTunnel
    {
        void Wash(IVehicle vehicle, IWashProgram program);
    }

    public class WashTunnel : IWashTunnel
    {
        private readonly ICentralControllerUnit _centralControllerUnit;

        public WashTunnel(ICentralControllerUnit centralControllerUnit)
        {
            _centralControllerUnit = centralControllerUnit;
        }

        public void Wash(IVehicle vehicle, IWashProgram program)
        {
            IWashStep[] washSteps = program.GetWashSteps().ToArray();

            for (int i = 0; i < washSteps.Length - 1; i++)
            {
                washSteps[i].NextStep(washSteps[i + 1]);
            }

            washSteps[0].Execute(vehicle);
            _centralControllerUnit.Transmit(Signal.VehicleReady);
        }
    }
}
