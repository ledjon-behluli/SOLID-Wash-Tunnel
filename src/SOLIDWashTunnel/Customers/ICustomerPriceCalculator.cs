using SOLIDWashTunnel.Invoices;
using SOLIDWashTunnel.WashPrograms;
using System.Linq;

namespace SOLIDWashTunnel.Customers
{
    public interface ICustomerPriceCalculator
    {
        int Discount { get; }
        decimal Calculate(IWashProgram program, Currency currency);
    }

    public abstract class CustomerPriceCalculator : ICustomerPriceCalculator
    {
        public abstract int Discount { get; }
        protected readonly ICurrencyRateConverter converter;

        public CustomerPriceCalculator(ICurrencyRateConverter converter)
        {
            this.converter = converter;
        }

        public virtual decimal Calculate(IWashProgram program, Currency currency)
        {
            decimal totalPrice = program.GetWashSteps().Sum(x => x.Price);
            return converter.Convert(totalPrice, currency);
        }
    }

    public class IndividualPriceCalculator : CustomerPriceCalculator
    {
        public override int Discount => 0;

        public IndividualPriceCalculator(ICurrencyRateConverter converter)
            : base(converter)
        {

        }
    }

    public class CompanyPriceCalculator : CustomerPriceCalculator
    {
        public override int Discount => 20;

        public CompanyPriceCalculator(ICurrencyRateConverter converter)
            : base(converter)
        {

        }

        public override decimal Calculate(IWashProgram program, Currency currency)
        {
            decimal totalPrice = base.Calculate(program, currency);
            return totalPrice - ((Discount / 100m) * totalPrice);
        }
    }
}
