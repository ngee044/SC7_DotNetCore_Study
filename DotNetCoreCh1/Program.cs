using System;
using Packt.CS7;
using static System.Console;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DotNetCoreCh1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLogProvider());

                IQueryable<Category> cats = db.Categories.Include(data => data.Products);

                foreach (Category c in cats)
                {
                    WriteLine("{0} has {1} products", c.CategoryName, c.Products.Count);
                }

                WriteLine("==================Price=================");
                string input;
                decimal price;
                do
                {
                    Write("Enter a product price: ");
                    input = ReadLine();
                } while (!decimal.TryParse(input, out price));

                IQueryable<Product> prods = db.Products.Where(product => product.UnitPrice > price).OrderByDescending(product => product.UnitPrice);
              
                foreach(Product item in prods)
                {
                    WriteLine($"{item.ProductID} : {item.ProductName} costs {item.UnitPrice:$#,##0.00}");
                }
            }
        }
    }
}
