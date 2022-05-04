using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace lab_9
{
    
    public record Car
    {
        public int Id { get; set; }
        public String Model { get; set; }
        public decimal Power { get; set; }
    }
    
    public record User
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public String Name { get; set; }
    }
    class AppContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DATASOURCE = d:\\database\\sqltest.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("cars");
            modelBuilder.Entity<User>().ToTable("users");
           
        }
        public void InsertData()
        {
            
                this.Cars.Add(new Car() { Model = "Audi A4", Power = 200 });
                this.Cars.Add(new Car() { Model = "Ford K", Power = 90 });
                this.Cars.Add(new Car() { Model = "Fiat 500", Power = 100 });
                this.Cars.Add(new Car() { Model = "Kia", Power = 120 });
                this.Users.Add(new User() { CarId = 1, Name = "Karol" });
                this.Users.Add(new User() { CarId = 2, Name = "Ewa" });
                this.Users.Add(new User() { CarId = 3, Name = "Adam" });
                this.SaveChanges();
              

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AppContext context = new AppContext();
            context.Database.EnsureCreated();
            //context.InsertData();
            IQueryable<Car> cars = from car in context.Cars
                                   where car.Power > 100
                                   select car;
            Console.WriteLine(string.Join("\n", cars));

            var carUsers = from car in context.Cars
                           join user in context.Users
                           on car.Id equals user.CarId
                           where car.Power > 100
                           //select new { UserId = user.Id, CarModel = car.Model, UserName = user.Name };
                           select new { CarPower = car.Power, CarModel = car.Model, UserName = user.Name };
            Console.WriteLine(string.Join("\n", carUsers));

            var carList = context.Cars.Join(
                context.Users,
                c => c.Id,
                u => u.CarId,
                (c, u) => new { Moc = c.Power, Model = c.Model, Nazwa = u.Name }
                );

            Console.WriteLine(string.Join("\n", carList));

            string xml = 
                "<cars>" +
                    "<car>" +
                        "<id>1</id>" +
                        "<model>audi</model>" +
                        "<power>100</power>" +
                    "</car>" +
                    "<car>" +
                        "<id>2</id>" +
                        "<model>fiat</model>" +
                        "<power>120</power>" +
                    "</car>" +
                "</cars>";

            XDocument doc = XDocument.Parse(xml);
            IEnumerable<XElement> modelNodes = 
                doc
                .Elements("cars")
                .Elements("car")
                .Elements("model");
            foreach(var element in modelNodes)
            {
                Console.WriteLine(element.Value);
                
            }
            IEnumerable<XElement> powerNodes =
                doc
                .Elements("cars")
                .Elements("car")
                .Elements("power");
            foreach (var element in powerNodes)
            {
                Console.WriteLine(element.Value);

            }

            var xmlCars = XDocument.Parse(xml)
                .Elements("cars")
                .Elements("car")
                .Select(carNode => new { Id = carNode.Elements("id").First().Value, Model = carNode.Elements("model").First().Value, Power = carNode.Elements("power").First().Value });

            Console.WriteLine(string.Join("\n", xmlCars));

            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            string xmlRates = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C");
            doc = XDocument.Parse(xmlRates);
            var rates = doc
                .Elements("ArrayOfExchangeRatesTable")
                .Elements("ExchangeRatesTable")
                .Elements("Rates")
                .Elements("Rate")
                .Select(rate => new { Code = rate.Elements("Code").First().Value, Bid = rate.Elements("Bid").First().Value, Ask = rate.Elements("Ask").First().Value });

            Console.WriteLine(string.Join("\n", rates));




        } 
    }
}
