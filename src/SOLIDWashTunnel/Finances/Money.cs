using System;
using System.ComponentModel;

namespace SOLIDWashTunnel
{
    public class Money : IEquatable<Money>
    {
        public Currency Currency { get; }
        public decimal Amount { get; }


        public Money(Currency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }


        public static Money Create(decimal amount) 
            => new Money(Currency.USD, amount);

        public static Money Zero(Currency currency) 
            => new Money(currency, 0.0m);

        public static Money operator *(decimal amount, Money money)
            => new Money(money.Currency, money.Amount * amount);

        public static Money operator +(Money money1, Money money2)
        {
            if (money1.Currency != money2.Currency)
            {
                throw new InvalidOperationException("Can not add two money objects with different currencies.");
            }

            return new Money(money1.Currency, money1.Amount + money2.Amount);
        }

        public static Money operator -(Money money1, Money money2)
        {
            if (money1.Currency != money2.Currency)
            {
                throw new InvalidOperationException("Can not subtract two money objects with different currencies.");
            }

            return new Money(money1.Currency, money1.Amount - money2.Amount);
        }


        public bool Equals(Money other)
        {
            if (ReferenceEquals(other, null)) 
                return false;

            if (ReferenceEquals(other, this)) 
                return true;

            return Currency.Equals(other.Currency) && Amount.Equals(other.Amount);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public override int GetHashCode()
        {
            return Currency.GetHashCode() ^ Amount.GetHashCode();
        }

        public override string ToString()
        {
            var field = Currency.GetType().GetField(Currency.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            string symbol = attributes != null && attributes.Length > 0 ?
                            attributes[0].Description : Currency.ToString();

            return $"{Amount}{symbol}";
        }
    }
}
