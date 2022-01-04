using SOLIDWashTunnel.Finances;
using SOLIDWashTunnel.Programs;
using System.Text;

namespace SOLIDWashTunnel.Invoices
{
    /* 
    * Pattern:
    *   Builder
    *   
    * Reason: 
    *   Create a potential complex invoice object step by step. 
    *   Paired with the Fluent interface, this gives a nice way of expressing intent of the build process of an invoice.
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Builder_pattern
    *   https://en.wikipedia.org/wiki/Fluent_interface
    */

    public class InvoiceBuilder : IInvoiceBuilder, IIndividualNamePicker, ICompanyNamePicker, IProgramSelector, ICurrencyPicker, IAmountCalculator, IInvoicePrinter
    {
        private int _discount;
        private CustomerType _customerType;
        private Currency _currency;
        private readonly Invoice _invoice;

        private readonly ICurrencyRateConverter _converter;
        private readonly IPriceCalculatorFactory _calculatorFactory;

        public InvoiceBuilder(
            ICurrencyRateConverter converter,
            IPriceCalculatorFactory calculatorFactory)
        {
            _invoice = new Invoice();
            _converter = converter;
            _calculatorFactory = calculatorFactory;
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
            _currency = currency;
            return this;
        }

        public IInvoicePrinter Calculate()
        {
            IPriceCalculator calculator = _calculatorFactory.Create(_customerType);
            _invoice.Price = calculator.Calculate(_invoice.WashProgram, _currency);
            _discount = calculator.Discount;

            return this;
        }

        public virtual string Build()
        {
            var builder = new StringBuilder();
        
            builder.AppendLine($"Recepient: {_invoice.Recepient}");
            builder.AppendLine($"Program type: {_invoice.WashProgram.Name}");
            builder.AppendLine("-----------------------------");

            foreach (var washStep in _invoice.WashProgram.GetWashSteps())
                builder.AppendLine($" * {washStep.GetDescription()} - {_converter.Convert(washStep.Price, _currency)}");

            builder.AppendLine("-----------------------------");
            builder.AppendLine($"Total price: {_invoice.Price}");
            builder.AppendLine($"Applied discount: {_discount}%");

            return builder.ToString();
        }
    }
}
