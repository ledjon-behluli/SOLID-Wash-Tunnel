using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Invoices;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IUserPanel
    {
        IClientInformationCollector SelectProgram(ProgramType type);
    }

    public interface IClientInformationCollector
    {
        IWashProcessStarter ForIndividual(string firstName, string lastName, Currency preferedCurrecy);
        IWashProcessStarter ForCompany(string companyName, Currency preferedCurrecy);
    }

    public interface IWashProcessStarter
    {
        void Start(IVehicle vehicle);
    }
    
    public class UserPanel : IUserPanel, IClientInformationCollector, IWashProcessStarter
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

        public IClientInformationCollector SelectProgram(ProgramType type)
        {
            IWashProgram program = _programFactory.Create(type);
            _controlUnit.Transmit(new WashProgramSelectedSignal(program));

            return this;
        }

        public IWashProcessStarter ForIndividual(string firstName, string lastName, Currency preferedCurrecy)
        {
            _controlUnit.Transmit(new IndividualCustomerInfoEnteredSignal(firstName, lastName, preferedCurrecy));
            return this;
        }

        public IWashProcessStarter ForCompany(string companyName, Currency preferedCurrecy)
        {
            _controlUnit.Transmit(new CompanyCustomerInfoEnteredSignal(companyName, preferedCurrecy));
            return this;
        }

        public void Start(IVehicle vehicle)
        {
            _controlUnit.Transmit(new VehicleWashingStartedSignal(vehicle));
        }
    }

    #region Signals

    public class WashProgramSelectedSignal : IControlUnitSignal
    {
        public IWashProgram Program { get; }

        public WashProgramSelectedSignal(IWashProgram program)
        {
            Program = program;
        }
    }

    public class IndividualCustomerInfoEnteredSignal : IControlUnitSignal
    {
        public string FirstName { get; }
        public string LastName { get; }
        public Currency PreferredCurrency { get; }

        public IndividualCustomerInfoEnteredSignal(string firstName, string lastName, Currency preferredCurrency)
        {
            FirstName = firstName;
            LastName = lastName;
            PreferredCurrency = preferredCurrency;
        }
    }

    public class CompanyCustomerInfoEnteredSignal : IControlUnitSignal
    {
        public string CompanyName { get; }
        public Currency PreferredCurrency { get; }

        public CompanyCustomerInfoEnteredSignal(string companyName, Currency preferredCurrency)
        {
            CompanyName = companyName;
            PreferredCurrency = preferredCurrency;
        }
    }

    public class VehicleWashingStartedSignal : IControlUnitSignal
    {
        public IVehicle Vehicle { get; }

        public VehicleWashingStartedSignal(IVehicle vehicle)
        {
            Vehicle = vehicle;
        }
    }

    #endregion
}
