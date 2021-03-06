using SOLIDWashTunnel.Programs;
using System.Linq;

namespace SOLIDWashTunnel.Finances
{
    /* 
    * Pattern: 
    *   Strategy
    *   
    * Reason: 
    *   Instead of implementing a single algorithm directly, code receives runtime instructions as to which in a family of algorithms to use.
    *   Switch different price calculation strategies (individual & company) at runtime, for the end invoice generation.
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Strategy_pattern
    */

    public interface IPriceCalculator
    {
        int Discount { get; }
        Money Calculate(IWashProgram program, Currency currency);
    }

    public abstract class PriceCalculator : IPriceCalculator
    {
        public abstract int Discount { get; }
        protected readonly ICurrencyRateConverter converter;

        public PriceCalculator(ICurrencyRateConverter converter)
        {
            this.converter = converter;
        }

        public virtual Money Calculate(IWashProgram program, Currency currency)
        {
            Money totalPrice = program
                .GetWashSteps()
                .Select(x => x.Price)
                .Aggregate((x, y) => x + y);

            return converter.Convert(totalPrice, currency);
        }
    }

    public class IndividualPriceCalculator : PriceCalculator
    {
        public override int Discount => 0;

        public IndividualPriceCalculator(ICurrencyRateConverter converter)
            : base(converter)
        {

        }
    }

    public class CompanyPriceCalculator : PriceCalculator
    {
        public override int Discount => 20;

        public CompanyPriceCalculator(ICurrencyRateConverter converter)
            : base(converter)
        {

        }

        public override Money Calculate(IWashProgram program, Currency currency)
        {
            Money totalPrice = base.Calculate(program, currency);
            return totalPrice - (Discount / 100m * totalPrice);
        }
    }
}
