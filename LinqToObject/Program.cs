using System;
using System.Linq;
using System.Collections.Generic;

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
            if(!string.IsNullOrEmpty(description))
            {
                Console.WriteLine(description);
            }
            Console.Write(" ");
            Console.WriteLine(string.Join(", ", cohort.ToArray()));
        }

        public static void LinqToObject()
        {
            var names = new string[] { "Michael", "Pam", "Jim",
             "Dwight", "Angela", "Kevin", "Toby", "Creed"};
            //var query = names.Where(new Func<string, bool>(NameLongerThanFour));
            var query = names
                .Where(new Func<string, bool>(name => name.Length > 4))
                .OrderBy(name => name.Length)
                .ThenBy(name => name);

            foreach (var item in query)
            {
                //Console.WriteLine(item);
            }

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
        }



        static void Main(string[] args)
        {
            
        }

    }
}
