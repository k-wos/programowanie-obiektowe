using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_8
{

    record Student(string Name, int Ects);
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 4, 5, 16, 8, 11, 2, 7, 8, 9 };
            Predicate<int> predicate = n =>
            {
                Console.WriteLine("Predykat dla " + n);
                return n % 2 == 0 && n > 4;
            };
            IEnumerable<int> enumerable = 
                from n in ints
                where predicate.Invoke(n)
                select n;
            int sum = enumerable.Sum();
            Console.WriteLine("Suma = "+sum);
            Console.WriteLine("Ewaluacja");
            Console.WriteLine(string.Join(", ", enumerable));

            string[] strings = { "aa", "bbb", "cc", "ddddd", "abc", "bab" };

            IEnumerable<string> enumerable1 = 
                from str in strings
                where str.Length == 3
                select "str = " + str.ToUpper();
            Console.WriteLine(string.Join(", ",enumerable1));
            Console.WriteLine(string.Join(", ",
                from i in ints
                select i.ToString("X")
                ));

            Student[] students =
            {
                new Student("Ewa",12),
                new Student("Karol", 42),
                new Student("Adam", 62),
                new Student("Ola",22),
                new Student("Ewa", 62),
                new Student("Adam", 12)
            };

            Console.WriteLine(string.Join("\n",
                from s in students
                where s.Ects > 30
                orderby s.Name descending orderby s.Ects
                select s.Name
                ));

            IEnumerable<IGrouping<string, Student>> group = 
                from s in students
                group s by s.Name;
            foreach(var item in group)
            {
                Console.WriteLine(item.Key + " " + item.Count());
            }

            IEnumerable<(string Key, int)> namesGroup = 
                from s in students 
                group s by s.Name into names
                select(names.Key, names.Count());
            Console.WriteLine(string.Join("\n",namesGroup));

            IEnumerable<(int Key, int)> ectsGroup = from s in students
            group s by s.Ects into ectsGr
            select (ectsGr.Key, ectsGr.Count());

            Console.WriteLine(string.Join("\n", ectsGroup));

            IEnumerable<int> evens = ints.Where(n => n % 2 == 0).OrderBy(n => n);
            Console.WriteLine(string.Join(", ", evens));
            Console.WriteLine(string.Join("\n", students.OrderBy(s=>s.Name).ThenBy(s=>s.Ects)));
            IEnumerable<(int Key, int)> enumerable2 = students
                .GroupBy(s => s.Ects)
                .Select(gr => (gr.Key, gr.Count()));

            Student student = students.ElementAtOrDefault(10);
            Console.WriteLine(student);

            bool allPassed = students.All(s => s.Ects > 10);

            Console.WriteLine(string.Join(", ", ints.All(s => s % 2 == 0)));
            Console.WriteLine(string.Join(", ", ints.Any(s => s % 2 == 0)));

            Enumerable.Range(0, 100).Where(n => n % 2 == 0).Sum();

            Random random = new Random();
            random.Next(5);

            int[] vs = Enumerable.Range(0, 1000).Select(n => random.Next(10)).ToArray();

            

        }
    }
}
