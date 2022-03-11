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
            Console.WriteLine($"{money}");
            money.Equals(cost);
            Console.WriteLine();

            Money[] prices =
            {
                Money.Of(11, Currency.PLN),
                Money.Of(13, Currency.PLN),
                Money.Of(17, Currency.USD),
                Money.Of(12, Currency.USD),
                Money.Of(11, Currency.USD),
                Money.Of(12, Currency.EUR)
            };

            Array.Sort(prices);
            foreach(var x in prices)
            {
                Console.WriteLine(x.ToString());
            }
        }
    }
    public class PersonProperties
    {
        private string _name;

        private PersonProperties(string name)
        {
            _name = name;
        }
        
        public static PersonProperties Of(string name)
        {
            if(name != null && name.Length >= 3)
            {
                return new PersonProperties(name);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Za krótkie imie");
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null && value.Length >= 3)
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Za krótkie imie");
                }
            }
        }
        public override string ToString()
        {
            return _name;
        }


    }

    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }

    public class Money : IEquatable<Money>, IComparable<Money>
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
        public override string ToString()
        {
            return $"{_value} {_currency}";
        }

        public override bool Equals(object obj)
        {
            return obj is Money money &&
                   _value == money._value &&
                   _currency == money._currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _currency);
        }

        public bool Equals(Money other)
        {
            return  _value == other._value && _currency == other._currency;
        }

        public int CompareTo(Money other)
        {
            int result = _currency.CompareTo(other._currency);
            if(result == 0)
            {
                return _value.CompareTo(other._value);
            }
            else
            {
                return result;
            }
        }
    }

    public class Tank
    {
        public readonly int Capacity;
        private int _level;

        public Tank(int capacity)
        {
            Capacity = capacity;
        }

        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if(value < 0 || value > Capacity)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _level = value;
            }
        }

        public bool Refuel(int amount)
        {
            if( amount < 0)
            {
                return false;
            }
            if(_level + amount > Capacity)
            {
                return false;
            }
            _level += amount;
            return true;
        }

        public bool Consume(int amount)
        {
            if(amount > _level)
            {
                return false;
            }
            if(amount < 0)
            {
                return false;
            }

            _level -= amount;
            return true;
        }

        public bool Refuel(Tank sourceTank, int amount)
        {
            if(amount + _level > Capacity)
            {
                return false;
            }
            if(sourceTank._level - amount < 0)
            {
                return false;
            }
            if(amount < 0)
            {
                return false;
            }
            sourceTank._level -= amount;
            _level += amount;
            return true;

        }
        public override string ToString()
        {
            return $"Capacity = {Capacity}, Level = {_level}";
        }
    }

}
