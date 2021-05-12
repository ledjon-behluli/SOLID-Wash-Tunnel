using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IUserPanel
    {
        void SelectProgram(ProgramType type);
        void Start(IVehicle vehicle);
    }
    
    public class UserPanel : IUserPanel
    {
        private readonly ICentralControllerUnit _centralControllerUnit;
        private readonly IWashProgramFactory _programFactory;

        public UserPanel(
            ICentralControllerUnit centralControllerUnit,
            IWashProgramFactory programFactory)
        {
            _centralControllerUnit = centralControllerUnit;
            _programFactory = programFactory;
        }

        public void SelectProgram(ProgramType type)
        {
            IWashProgram program = _programFactory.Create(type);
            _centralControllerUnit.Transmit(Signal.WashProgramSelected, program);
        }

        public void Start(IVehicle vehicle)
            => _centralControllerUnit.Transmit(Signal.StartWashing, vehicle);
    }
}
