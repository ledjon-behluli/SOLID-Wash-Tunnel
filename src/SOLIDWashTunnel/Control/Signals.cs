using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.Programs;
using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.Control
{
    public class WashProgramSelectedSignal : IControlUnitSignal
    {
        public IWashProgram Program { get; }

        public WashProgramSelectedSignal(IWashProgram program)
        {
            Program = program;
        }
    }

    public abstract class CustomerInfoEnteredSignal : IControlUnitSignal
    {
        public Currency PreferredCurrency { get; }

        public CustomerInfoEnteredSignal(Currency preferredCurrency)
        {
            PreferredCurrency = preferredCurrency;
        }
    }

    public class IndividualCustomerInfoEnteredSignal : CustomerInfoEnteredSignal
    {
        public string FirstName { get; }
        public string LastName { get; }


        public IndividualCustomerInfoEnteredSignal(string firstName, string lastName, Currency preferredCurrency)
            : base(preferredCurrency)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public class CompanyCustomerInfoEnteredSignal : CustomerInfoEnteredSignal
    {
        public string CompanyName { get; }

        public CompanyCustomerInfoEnteredSignal(string companyName, Currency preferredCurrency)
            : base(preferredCurrency)
        {
            CompanyName = companyName;
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

    public class VehicleReadySignal : IControlUnitSignal
    {

    }
}
