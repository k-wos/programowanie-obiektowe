using System;
using System.Collections.Generic;

namespace lab_6
{
    class Student:IComparable<Student>
    {
        public string Name { get; set; }
        public int Ects { get; set; }

        public int CompareTo(Student other)
        {
            return Name.CompareTo(other.Name);
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Student Equals");
            return obj is Student student &&
                   Name == student.Name &&
                   Ects == student.Ects;
        }
        public override int GetHashCode()
        {
            Console.WriteLine("Student HashCode");
            return HashCode.Combine(Name, Ects);
        }

        public override string ToString()
        {
            return $"Name = {Name}, Ects = {Ects}";
        }
    }
    //record Student
    //{
    //    public string Name { get; set; }
    //    public int Ects { get; set; }

        
    //}
    class Program
    {
        static void Main(string[] args)
        {
            ICollection<string> names = new List<string>();
            names.Add("Ewa");
            names.Add("Patryk");
            names.Add("Robert");
            Console.WriteLine(names.Contains("Patryk"));
            names.Remove("Ewa");
            foreach(string name in names)
            {
                Console.WriteLine(name);
            }

            ICollection<Student> students = new List<Student>();
            Console.WriteLine("-----------------------------");
            students.Add(new Student() { Name = "Jakub", Ects = 21 });
            students.Add(new Student() { Name = "Julia", Ects = 11 });
            students.Add(new Student() { Name = "Marcel", Ects = 31 });
            //Musi byc zadeklarowana metoda equals
            Console.WriteLine(students.Contains(new Student() { Name = "Jakub", Ects = 21 }));
            students.Remove(new Student() { Name = "Jakub", Ects = 21 });
            foreach(Student student in students)
            {
                Console.WriteLine(student.Name + " "+ student.Ects);
            }

            Console.WriteLine("-----------------------------");

            List<Student> list = (List<Student>)students;
            Console.WriteLine(list[0]);
            list.Insert(0, new Student() { Name = "Piotr", Ects = 45 });
            int index = list.IndexOf(new Student() { Name = "Marcel", Ects = 31 });
            Console.WriteLine(index);

            Console.WriteLine("-----------------------------");

            ISet<string> setNames = new HashSet<string>();
            setNames.Add("Ewa");
            setNames.Add("Patryk");
            setNames.Add("Robert");
            setNames.Add("Robert");
            Console.WriteLine(string.Join(",", setNames));

            Console.WriteLine("-----------------------------");

            ISet<Student> studentGroup = new HashSet<Student>();
            studentGroup.Add(list[0]);
            studentGroup.Add(list[1]);
            studentGroup.Add(list[2]);
            studentGroup.Add(new Student() { Name = "Marcel", Ects = 31 });
            foreach (var student in studentGroup)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("------------Contains------------");
            Console.WriteLine(studentGroup.Contains(list[2]));
            Console.WriteLine("--------------------------------");
            list.Add(new Student() { Name = "Ela", Ects = 34 });
            list.Add(new Student() { Name = "Marek", Ects = 16 });
            List<Student> result = new List<Student>();
            //foreach(Student student in list)
            //{
            //    if (studentGroup.Contains(student))
            //    {
            //        result.Add(student);
            //    }
            //}
            ISet<Student> set = new HashSet<Student>(list);
            ISet<Student> commonSet = new HashSet<Student>(studentGroup);
            commonSet.IntersectWith(set);
            Console.WriteLine(string.Join(", ",commonSet));
            Console.WriteLine("--------------------------------");
            ISet<Student> sortedSet = new SortedSet<Student>(studentGroup);
            sortedSet.Add(new Student() { Name = "Andrzej", Ects = 61 });
            foreach(Student student in sortedSet)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("--------------------------------");

            Dictionary<Student, string> phones = new Dictionary<Student, string>();
            phones[list[0]] = "24142312";
            phones[list[2]] = "83176322";
            phones[new Student() { Name = "Kamil", Ects = 15 }] = "7262152526";

            foreach(var item in phones)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }

            Console.WriteLine(string.Join(", ",phones.Keys));
            Console.WriteLine(string.Join(",", phones.Values));
        }
    }
}
