using System;
using System.Collections.Generic;
using System.Linq;

namespace MathsCore.Statistics
{
    public static class StatisticsExtensions
    {
        // ReSharper disable InconsistentNaming
        public static double TTest(this IEnumerable<double> dataA, IEnumerable<double> dataB)
        // ReSharper restore InconsistentNaming
        {
            var enumerableA = dataA as IList<double> ?? dataA.ToList();
            var nA = enumerableA.Count();
            var enumerableB = dataB as IList<double> ?? dataB.ToList();
            var nB = enumerableB.Count();
            var sA = enumerableA.StdDev();
            var sB = enumerableB.StdDev();
            var pooledEstimate = Math.Sqrt(((nA - 1) * sA * sA + (nB - 1) * sB * sB)) /
                                 (nA + nB - 2);
            return Math.Abs(enumerableA.Average() - enumerableB.Average()) /
                       (pooledEstimate * Math.Sqrt(1 / (double)nA + 1 / (double)nB));
        }

        public static double StdDev(this IEnumerable<double> data)
        {
            var enumerable = data as IList<double> ?? data.ToList();
            var count = enumerable.Count();
            if (count <= 1) return 0;
            var avg = enumerable.Average();
            var sum = enumerable.Sum(d => (d - avg) * (d - avg));
            return Math.Sqrt(sum / count);
        }
    }
}