using SOLIDWashTunnel.Customers;
using SOLIDWashTunnel.Programs;
using System.Text;

namespace SOLIDWashTunnel.Invoices
{
    /* 
    * Pattern: Builder
    * Reason: TODO
    * Learn more: https://refactoring.guru/design-patterns/strategy
    */

    public class InvoiceBuilder : IInvoiceBuilder, IIndividualNamePicker, ICompanyNamePicker, IProgramSelector, ICurrencyPicker, IAmountCalculator, IInvoicePrinter
    {
        private int _discount;
        private CustomerType _customerType;
        private readonly Invoice _invoice;
        private readonly ICurrencyRateConverter _converter;
        private readonly ICustomerPriceCalculatorFactory _calculatorFactory;

        // Note: The 'ICustomerPriceCalculatorFactory' is being injected via the constructor since we are using an IoC container that supports ctor injections only.
        // With a more sophisticated IoC container it would be best to inject this service in the 'IAmountCalculator.Calculate()'.
        public InvoiceBuilder(
            ICurrencyRateConverter converter,
            ICustomerPriceCalculatorFactory calculatorFactory)
        {
            _invoice = new Invoice();
            this._converter = converter;
            this._calculatorFactory = calculatorFactory;
        }

        public IIndividualNamePicker CreateForIndividual()
        {
            _customerType = CustomerType.Individual;
            return this;
        }

        public ICompanyNamePicker CreateForCompany()
        {
            _customerType = CustomerType.Company;
            return this;
        }

        public IProgramSelector WithName(string firstName, string lastName)
        {
            _invoice.Recepient = $"{firstName} {lastName}";
            return this;
        }

        public IProgramSelector WithName(string companyName)
        {
            _invoice.Recepient = companyName;
            return this;
        }

        public ICurrencyPicker Select(IWashProgram program)
        {
            _invoice.WashProgram = program;
            return this;
        }

        public IAmountCalculator Choose(Currency currency)
        {
            _invoice.Currency = currency;
            return this;
        }

        public IInvoicePrinter Calculate()
        {
            ICustomerPriceCalculator calculator = _calculatorFactory.Create(_customerType);
            _invoice.Price = calculator.Calculate(_invoice.WashProgram, _invoice.Currency);
            _discount = calculator.Discount;

            return this;
        }

        // TODO: Extract to send Mediator.Send() to a real printer.
        public string Print()
        {
            var builder = new StringBuilder();
        
            builder.AppendLine($"Recepient: {_invoice.Recepient}");
            builder.AppendLine("Wash steps applied and prices:");
            builder.AppendLine("-----------------------------");

            foreach (var washStep in _invoice.WashProgram.GetWashSteps())
                builder.AppendLine($" * {washStep.Describe()} - {_converter.Convert(washStep.Price, _invoice.Currency)}{_invoice.Currency.GetDescription()}");

            builder.AppendLine("-----------------------------");
            builder.AppendLine($"Total price: {_invoice.Price}{_invoice.Currency.GetDescription()}");
            builder.AppendLine($"Discount: {_discount}%");

            return builder.ToString();
        }
    }
}
