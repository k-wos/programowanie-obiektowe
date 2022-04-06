using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_5
{
    class Team : IEnumerable<string>
    {
        public string Leader { get; init; }
        public string ScrumMaster { get; init; }
        public string Developer { get; init; }

        public IEnumerator<string> GetEnumerator()
        {
            yield return Leader;
            yield return ScrumMaster;
            yield return Developer;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    class Parking : IEnumerable<string>
    {
        private string[] _cars = { "Fiat", "Audi", "BMW", null, "Ford" };
        public string this[char index]
        {
            get
            {
                return _cars[index - 'a'];
            }
            set
            {
                _cars[index - 'a'] = value;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (string car in _cars)
            {
                if (car != null)
                {
                    yield return car;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Hello World!");
                Team team = new Team() { Leader = "Nowak", Developer = "Kos", ScrumMaster = "Marzec" };
                IEnumerator<string> members = team.GetEnumerator();

                foreach (var member in team)
                {
                    Console.WriteLine(member);
                }
                Parking parking = new Parking();
                parking['d'] = "Dacia";
                Console.WriteLine(string.Join(", ",parking));
            }
        }

    }
}