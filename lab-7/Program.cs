using System;

namespace lab_7
{
    delegate double operation(double a, double b);
    delegate bool stringPredictate(string str);
    class Program
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }
        
        public static double Mul(double a, double b)
        {
            return a * b;
        }

        public static bool FiveCharacters(string str)
        {
            return str.Length == 5;
        }
        static void Main(string[] args)
        {
            operation op = Add;
            Console.WriteLine(op.Invoke(4, 6));
            op = Mul;
            Console.WriteLine(op.Invoke(4, 6));
            stringPredictate predicate = FiveCharacters;
            Console.WriteLine(predicate.Invoke("abcde"));
            Func<double, double, double> funcOperator = delegate(double a, double b)
            {
                return a* b;
            };
            Func<int, string> FormatDelegate = delegate (int number)
            {
                return string.Format("{0:x}", number);
            };
            Console.WriteLine(FormatDelegate.Invoke(14));
            Predicate<string> OnlyFive = FiveCharacters;
            Predicate<int> InRange = delegate (int number)
            {
                return number >= 0 && number <= 100;
            };
            Console.WriteLine(InRange.Invoke(91));

            Func<int, int, int, bool> InRangeUpdate = delegate (int value, int min, int max)
            {
                return value >= min && value <= max;
            };

            Console.WriteLine(InRangeUpdate.Invoke(91,0,100));

            Action<string> Print = delegate (string str)
            {
                Console.WriteLine(str);
            };
            Print.Invoke("Action");

            Action<string> PrintLambda = s => Console.WriteLine(s);

            Func<int, int, int, bool> InRangeLambda = (value, min, max) => value >= min && value <= max;
            Console.WriteLine(InRangeLambda.Invoke(91,100,100));

            operation Div = (a, b) =>
            {
                if (b != 0)
                {
                    return a / b;
                }
                else
                {
                    throw new Exception("b is 0!");
                }
            };
            Console.WriteLine(Div.Invoke(5,3));
        }
    }
}
