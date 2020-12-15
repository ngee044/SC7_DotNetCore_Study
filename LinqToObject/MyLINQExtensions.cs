using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LinqToObject
{
    public static class MyLINQExtensions
    {
        public static IEnumerable<T> ProcessSequence<T>(this IEnumerable<T> sequence)
        {
            return sequence;
        }

        public static long SummariseSequence<T>(this IEnumerable<T> sequence)
        {
            return sequence.LongCount();
        }
    }
}
