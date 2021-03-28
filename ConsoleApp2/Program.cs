using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    class Purchase
    {
        public Purchase(string name, double price, int weight)
        {
            Name = name;
            Price = price;
            Weight = weight;
        }

        public string Name { get; set; }
        public double Price { get; set; }
        public int Weight { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var drivers = new List<Purchase>()
            {
                new Purchase("HAM", 33.5, 20),
                new Purchase("VER", 24.8, 19),
                new Purchase("BOT", 23.6, 18),
                new Purchase("PER", 18.4, 17),
                new Purchase("RIC", 17.3, 16),
                new Purchase("LEC", 16.8, 15),
                new Purchase("VET", 16.2, 14),
                new Purchase("ALO", 15.6, 13),
                new Purchase("SAI", 14.4, 12),
                new Purchase("STR", 13.9, 11),
                new Purchase("NOR", 13.1, 10),
                new Purchase("GAS", 11.7, 9),
                new Purchase("OCO", 10.1, 8),
                new Purchase("RAI", 9.6, 7),
                new Purchase("TSU", 8.8, 6),
                new Purchase("GIO", 7.9, 5),
                new Purchase("LAT", 6.5, 4),
                new Purchase("RUS", 6.2, 3),
                new Purchase("SCH", 5.8, 2),
                new Purchase("MAZ", 5.5, 1)
            };

            var constructors = new List<Purchase>()
            {
                new Purchase("Mercedes", 38.0, 18),
                new Purchase("Red Bull", 25.9, 16),
                new Purchase("McLaren", 18.9, 14),
                new Purchase("Ferrari", 18.1, 12),
                new Purchase("Aston Martin", 17.6, 10),
                new Purchase("Alpine", 15.4, 8),
                new Purchase("Alpha Tauri", 12.7, 4),
                new Purchase("Alfa Romeo", 8.9, 2),
                new Purchase("Williams", 6.3, 1),
                new Purchase("HAAS", 6.1, 0)
            };

            double cash = 100;

            var result = new List<(int weight, double price, List<Purchase> drivers, Purchase constructor)>();

            foreach (var constructor in constructors)
            {
                Console.WriteLine($"evaluating {constructor.Name}");
                var budget = cash;
                budget -= constructor.Price;

                var driverOptions = GetDriverOptions(budget, drivers);
                foreach (var options in driverOptions)
                {
                    result.Add((weight: options.Sum(o => o.Weight) + constructor.Weight, price: options.Sum(o => o.Price) + constructor.Price, drivers: options.ToList(), constructor: constructor));
                }
            }

            var final = result.OrderByDescending(o => o.weight);
            foreach (var a in final)
            {
                Console.WriteLine("================");
                Console.WriteLine($"Full weight: {a.weight}");
                Console.WriteLine($"Full price: {a.price}");
                Console.WriteLine($"Constructor: {a.constructor.Name}");
                Console.WriteLine("Drivers");
                foreach (var d in a.drivers)
                {
                    Console.WriteLine($"   {d.Name}");
                }
            }

            Console.ReadKey();
        }

        static List<IEnumerable<Purchase>> GetDriverOptions(double budget, List<Purchase> drivers)
        {
            var allOptions = drivers.DifferentCombinations(5);
            var bestOptions = allOptions.Where(o => o.Sum(x => x.Price) <= budget).OrderByDescending(o => o.Sum(x => x.Weight)).Take(1).ToList();
            return bestOptions;
        }
    }

    public static class Ex
    {
        public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }
    }
}
