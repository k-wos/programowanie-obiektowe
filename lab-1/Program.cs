using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Money money = Money.Of(10, Currency.PLN);
            Money result = 5 * money;
            Console.WriteLine(result.Value);
            Money sum = money + result;
            Console.WriteLine(sum.Value);
            Console.WriteLine(sum < money);
            Console.WriteLine(money == Money.Of(10, Currency.PLN));
            Console.WriteLine(money != Money.Of(10, Currency.PLN));
            decimal amount = money;
            double cost = (double) money;
            float price = (float)money;
            Console.WriteLine(amount);
            Console.WriteLine(cost);
            Console.WriteLine(price);
        }
    }
    public class PersonProperties
    {
        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value.Length >= 2)
                {
                    firstName = value;
                }
            }
        }
    }

    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }

    public class Money 
    {
        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        public decimal Value
        {
            get
            {
                return _value;
            }
        }

        public Currency Currency
        {
            get
            {
                return _currency;
            }
        }

        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }

        public static Money operator *(Money a, decimal b)
        {
            return Money.Of(a._value * b, a._currency);
        }
        public static Money operator *(decimal b, Money a)
        {
            return Money.Of(a._value * b, a._currency);
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Different currencies");
            }
            else
            {
                return Money.Of(a._value + b._value,a._currency);
            }
        }
        public static bool operator >(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Different currencies");
            }
            else
            {
                return a.Value > b.Value;
            }
        }
        public static bool operator <(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Different currencies");
            }
            else
            {
                return a.Value < b.Value;
            }
        }
        public static bool operator ==(Money a, Money b)
        {
            return a.Value == b.Value && a.Currency == b.Currency; 
        }
        public static bool operator !=(Money a, Money b)
        {
            return !(a == b);
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }

        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }

        public static explicit operator float(Money money)
        {
            return (float)money.Value;
        }


    }

}
