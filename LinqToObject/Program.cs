using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Packt.CS7;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace LinqToObject
{
    class Program
    {
        //static bool NameLongerThanFour(string name)
        //{
        //    return name.Length > 4;
        //}

        private static void Output(IEnumerable<string> cohort, string description = "")
        {
            if (!string.IsNullOrEmpty(description))
            {
                Console.WriteLine(description);
            }
            Console.Write(" ");
            Console.WriteLine(string.Join(", ", cohort.ToArray()));
        }

        public static void LinqToObject()
        {
            //linq 메서드 사용 방법(*기초)
            var names = new string[] { "Michael", "Pam", "Jim",
             "Dwight", "Angela", "Kevin", "Toby", "Creed"};
            //var query = names.Where(new Func<string, bool>(NameLongerThanFour));
            var query = names
                .ProcessSequence()
                //.Where(new Func<string, bool>(name => name.Length > 4))
                .Where(name => name.Length > 4)
                .OrderBy(name => name.Length)
                .ThenBy(name => name);

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
#if (false)
            var cohort1 = new string[]
                {"Rachel", "Gareth", "jonathan", "Geore" };
            var cohort2 = new string[]
                {"Jack", "Stephen", "Daniel", "Jack", "Jared"};
            var cohort3 = new string[]
                {"Declan", "Jack", "Jack", "Jasmine", "Conor"};

            Output(cohort1, "Cohort 1");
            Output(cohort2, "Cohort 2");
            Output(cohort3, "Cohort 3");
            Console.WriteLine();

            Output(cohort2.Distinct(), "cohort2.Distinct(): removesduplicates");
            Output(cohort2.Union(cohort3), "cohort2.Union(): combines two sequences and removes any duplicates");
            Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3): combines two sequences but leaves in any duplicates");

            Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3): returns items that are in both sequences");
            Output(cohort2.Except(cohort3), "cohort2.Except(cohort3): removes items from the first sequence that arein the second sequence");
            Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"), "cohort1.Zip(cohort2, (c1, c2) => ${c1} matched with {c2}): matches items based on position in the sequence");
            //$"{c1} matched with {c2}"
#endif
        }

        public static void LinqToEntiti()
        {
            //그룹 사용 방법(join group)

            var db = new Northwind();
            var categories = db.Categories.Select(c => new { c.CategoryID, c.CategoryName }).ToArray();
            var products = db.Products.Select(p => new { p.ProductID, p.ProductName, p.CategoryID });
            var queryJoin = categories.Join(products, category => category.CategoryID, product => product.CategoryID,
                (c, p) => new { c.CategoryName, p.ProductName, p.ProductID })
                .OrderBy(cp => cp.ProductID);

            //foreach (var item in queryJoin)
            //{
            //    Console.WriteLine($"{item.ProductID}: {item.ProductName} is in {item.CategoryName}");
            //}

            var queryGroup = categories.GroupJoin(products, category => category.CategoryID, product => product.CategoryID,
                                                 (c, Products) => new { c.CategoryName, Products = products.OrderBy(p => p.ProductName) });

            foreach (var item in queryGroup)
            {
                Console.WriteLine($"{item.CategoryName} has {item.Products.Count()} products.");
                foreach (var product in item.Products)
                {
                    Console.WriteLine($"    {product.ProductName}");
                }
            }
        }

        public static void MultiThreadingLinq()
        {
            var watch = Stopwatch.StartNew();
            Console.Write("Press Enter to start");
            Console.ReadLine();
            watch.Start();
            IEnumerable<int> numbers = Enumerable.Range(1, 200_000_000);
            var squares = numbers.Select(number => number * 2).ToArray();
            // var squares = numbers.AsParallel()
            //      .Select(number => number * 2).ToArray();
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds:#,##0} elapsed milliseconds.");
        }

        static void LinqToXMLWrite()
        {
            var db = new Northwind();
            var productsForXml = db.Products.ToArray();

            var xml = new XElement("products",
                from p in productsForXml
                select new XElement("product",
                    new XAttribute("id", p.ProductID),
                    new XAttribute("price", p.UnitPrice),
                    new XElement("name", p.ProductName)));

            Console.WriteLine(xml.ToString());
        }

        public static void LinqToXmLRead()
        {
            try
            {
                XDocument doc = XDocument.Load("../../../settings.xml");

                var appSettings = doc.Descendants("appSettings").Descendants("add")
                .Select(node => new
                {
                    Key = node.Attribute("key").Value,
                    Value = node.Attribute("value").Value
                }).ToArray();

                foreach (var item in appSettings)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine($"File is Not Found");
                return;
            }
            
        }

        static void Main(string[] args)
        {
            LinqToXmLRead();
        }

    }
}
