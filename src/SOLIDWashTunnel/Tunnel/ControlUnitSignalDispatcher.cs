using SOLIDWashTunnel.Customers;
using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.IoC;
using SOLIDWashTunnel.Programs;

namespace SOLIDWashTunnel.Tunnel
{
    public class ControlUnitSignalDispatcher : 
        IControlUnitSignalHandler<WashProgramSelectedSignal>,
        IControlUnitSignalHandler<IndividualCustomerInfoEnteredSignal>,
        IControlUnitSignalHandler<CompanyCustomerInfoEnteredSignal>,
        IControlUnitSignalHandler<VehicleWashingStartedSignal>,
        IControlUnitSignalHandler<VehicleReadySignal>
    {
        private readonly IContainer _container;

        private IWashProgram _selectedProgram;
        public string _firstName;
        public string _lastName;
        public string _companyName;
        public Currency _preferredCurrency;
        public CustomerType _customerType;
        

        public ControlUnitSignalDispatcher(IContainer container)
        {
            _container = container;
        }


        public void Handle(WashProgramSelectedSignal signal)
        {
            _selectedProgram = signal.Program;
        }


        public void Handle(IndividualCustomerInfoEnteredSignal signal)
        {
            _firstName = signal.FirstName;
            _lastName = signal.LastName;
            _preferredCurrency = signal.PreferredCurrency;
            _customerType = CustomerType.Individual;
        }

        public void Handle(CompanyCustomerInfoEnteredSignal signal)
        {
            _companyName = signal.CompanyName;
            _preferredCurrency = signal.PreferredCurrency;
            _customerType = CustomerType.Company;
        }

        public void Handle(VehicleWashingStartedSignal signal)
        {
            _container.GetService<IWashTunnel>()
                      .Wash(signal.Vehicle, _selectedProgram);
        }

        public void Handle(VehicleReadySignal signal)
        {
            // TODO: use invoice builder;
        }
    }
}
