using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Customers;

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
        private readonly ICentralControllerUnit _centralControllerUnit;
        private readonly IWashProgramFactory _programFactory;

        public UserPanel(
            ICentralControllerUnit centralControllerUnit,
            IWashProgramFactory programFactory)
        {
            _centralControllerUnit = centralControllerUnit;
            _programFactory = programFactory;
        }

        public IClientInformationCollector SelectProgram(ProgramType type)
        {
            IWashProgram program = _programFactory.Create(type);
            _centralControllerUnit.Transmit(Signal.WashProgramSelected, program);

            return this;
        }

        public IWashProcessStarter ForIndividual(string firstName, string lastName, Currency preferedCurrecy)
        {
            InvoiceCustomerInfo info = new InvoiceCustomerInfo()
            {
                FirstName = firstName,
                LastName = lastName,
                PreferredCurrency = preferedCurrecy
            };

            _centralControllerUnit.Transmit(Signal.ClientInfosCollected, info);

            return this;
        }

        public IWashProcessStarter ForCompany(string companyName, Currency preferedCurrecy)
        {
            InvoiceCustomerInfo info = new InvoiceCustomerInfo()
            {
                CompanyName = companyName,
                PreferredCurrency = preferedCurrecy
            };

            _centralControllerUnit.Transmit(Signal.ClientInfosCollected, info);

            return this;
        }

        public void Start(IVehicle vehicle)
        {
            _centralControllerUnit.Transmit(Signal.StartWashing, vehicle);
        }
    }
}
