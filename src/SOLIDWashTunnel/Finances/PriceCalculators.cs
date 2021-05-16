using SOLIDWashTunnel.Programs;
using System.Linq;

namespace SOLIDWashTunnel.Finances
{
    /* 
    * Pattern: Strategy pattern
    * Reason: Switch different price calculation strategies at runtime.
    * Learn more: https://refactoring.guru/design-patterns/strategy
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
            return totalPrice - ((Discount / 100m) * totalPrice);
        }
    }
}
