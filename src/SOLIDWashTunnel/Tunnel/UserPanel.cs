using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Control;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IUserPanel
    {
        ICustomerInformationCollector SelectProgram(ProgramType type);
    }

    public interface ICustomerInformationCollector
    {
        IWashProcessStarter AsIndividual(string firstName, string lastName, Currency preferedCurrecy);
        IWashProcessStarter AsCompany(string companyName, Currency preferedCurrecy);
    }

    public interface IWashProcessStarter
    {
        void Start(IVehicle vehicle);
    }
    
    public class UserPanel : IUserPanel, ICustomerInformationCollector, IWashProcessStarter
    {
        private readonly IControlUnit _controlUnit;
        private readonly IWashProgramFactory _programFactory;

        public UserPanel(
            IControlUnit controlUnit,
            IWashProgramFactory programFactory)
        {
            _controlUnit = controlUnit;
            _programFactory = programFactory;
        }

        public ICustomerInformationCollector SelectProgram(ProgramType type)
        {
            IWashProgram program = _programFactory.Create(type);
            _controlUnit.Transmit(new WashProgramSelectedSignal(program));

            return this;
        }

        public IWashProcessStarter AsIndividual(string firstName, string lastName, Currency preferedCurrecy)
        {
            _controlUnit.Transmit(new IndividualCustomerInfoEnteredSignal(firstName, lastName, preferedCurrecy));
            return this;
        }

        public IWashProcessStarter AsCompany(string companyName, Currency preferedCurrecy)
        {
            _controlUnit.Transmit(new CompanyCustomerInfoEnteredSignal(companyName, preferedCurrecy));
            return this;
        }

        public void Start(IVehicle vehicle)
        {
            _controlUnit.Transmit(new VehicleWashingStartedSignal(vehicle));
        }
    }
}
