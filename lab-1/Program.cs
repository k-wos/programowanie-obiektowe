using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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
 
}

}
