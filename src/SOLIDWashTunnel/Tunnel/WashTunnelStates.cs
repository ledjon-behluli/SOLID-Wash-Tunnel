using System.Linq;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Programs.Steps;
using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Tunnel
{
    /* 
     * Pattern: 
     *   State
     *   
     * Reason: 
     *   Allow the wash tunnel to alter its behavior, when its internal state changes. 
     *   
     * Learn more: 
     *   https://en.wikipedia.org/wiki/State_pattern
     */

    public interface IWashTunnelState
    {
        void Handle(IVehicle vehicle, IWashProgram program);
    }

    public class FreeState : IWashTunnelState
    {
        private readonly IWashTunnel _washTunnel;

        public FreeState(IWashTunnel washTunnel)
        {
            _washTunnel = washTunnel;
        }

        public void Handle(IVehicle vehicle, IWashProgram program)
        {
            _washTunnel.TransitionState(new BusyState());
            IWashStep[] washSteps = program.GetWashSteps().ToArray();

            for (int i = 0; i < washSteps.Length - 1; i++)
            {
                washSteps[i].NextStep(washSteps[i + 1]);
            }

            washSteps[0].Visit(vehicle);
            _washTunnel.TransitionState(new FreeState(_washTunnel));
        }
    }

    public class BusyState : IWashTunnelState
    {
        public void Handle(IVehicle vehicle, IWashProgram program)
        {
            /* 
             * The wash tunnel can't accept washing a new vehicle, while an other vehicle is being washed from the tunnel.
             * So we simply ignore this request. We could extend this to queue a new washing process and start it when
             * the previous vehicle is ready.
             */
        }
    }
}
