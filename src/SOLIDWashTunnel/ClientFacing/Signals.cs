using SOLIDWashTunnel.Control;
using SOLIDWashTunnel.Programs;
using System;

namespace SOLIDWashTunnel.ClientFacing
{
    public class WashProgramSelectedSignal : ISignal
    {
        public IWashProgram Program { get; }

        public WashProgramSelectedSignal(IWashProgram program)
        {
            Program = program;
        }
    }

    public abstract class CustomerInfoEnteredSignal : ISignal
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

    public class VehicleWashingStartedSignal : ISignal
    {
        public IVehicle Vehicle { get; }
        public Action<string> InvoiceCallback { get; }

        public VehicleWashingStartedSignal(IVehicle vehicle, Action<string> invoiceCallback)
        {
            Vehicle = vehicle;
            InvoiceCallback = invoiceCallback;
        }
    }
}
