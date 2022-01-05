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
        private readonly IWashStepTracker _tracker;
        private readonly ICurrencyRateConverter _converter;
        private readonly IPriceCalculatorFactory _calculatorFactory;

        public InvoiceBuilder(
            IWashStepTracker tracker,
            ICurrencyRateConverter converter,
            IPriceCalculatorFactory calculatorFactory)
        {
            _invoice = new Invoice();
            _tracker = tracker;
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
            Money price = calculator.Calculate(_invoice.WashProgram, _currency);

            foreach (var washStep in _invoice.WashProgram.GetWashSteps())
            {
                if (!_tracker.HasStepBeenApplied(washStep))
                {
                    Money washStepPrice = washStep.Price;

                    if (price.Currency != washStep.Price.Currency)
                    {
                        washStepPrice = _converter.Convert(washStep.Price, _currency);
                    }

                    price -= washStepPrice;
                }
            }

            _invoice.Price = price;
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
            {
                if (_tracker.HasStepBeenApplied(washStep))
                {
                    builder.AppendLine($" * {washStep.GetDescription()} - {_converter.Convert(washStep.Price, _currency)}");
                }
            }

            builder.AppendLine("-----------------------------");
            builder.AppendLine($"Total price: {_invoice.Price}");
            builder.AppendLine($"Applied discount: {_discount}%");

            return builder.ToString();
        }
    }
}
