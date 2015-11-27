using System;
using System.Collections.Generic;
using System.Linq;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Sets;
using MathsCore.SimplicialComplex;

namespace MathsCore.Interfaces
{
    public interface IMetric<in T>
    {
        double Distance(T other);
    }

    /// <summary>
    /// extensions to the IMetric interface which implement notions of distance between sets of types implementing the interface
    /// </summary>
    public static class MetricExtensions
    {
        /// <summary>
        /// the closest element of a supplied set of objects of the given type to a given object of the given type
        /// </summary>
        /// <typeparam name="T">the given type which must extend IMetric in itself</typeparam>
        /// <param name="t">the given object of the given type</param>
        /// <param name="ts">the supplied set of objects of the given type</param>
        /// <returns>the closest element of a supplied set of objects of the given type to a given object of the given type</returns>
        public static T Closest<T>(this T t, IEnumerable<T> ts)
            where T : IMetric<T>
        {
            var minimizer = ts.Minimizer(u => u.Distance(t));
            return minimizer == null ? default(T) : minimizer.Item1;
        }

        /// <summary>
        /// returns the closest pair of elements from the operand and the supplied set, together with their distance
        /// </summary>
        /// <typeparam name="T"> the generic type contained in the sets</typeparam>
        /// <param name="set">the operand set</param>
        /// <param name="other">the other supplied set</param>
        /// <returns></returns>
        public static Tuple<Tuple<T, T>, double> Closest<T>(this IEnumerable<T> set, IEnumerable<T> other)
            where T : IMetric<T>
        {
            return set.Minimizers(other, (s1, s2) => s1.Distance(s2));
        }

        /// <summary>
        /// returns the two closest sets in a given collection of sets to a second supplied collection of sets,
        /// together with their distance
        /// </summary>
        /// <typeparam name="T">the type of objects contained in the sets in the collections</typeparam>
        /// <param name="set">the operand collection of sets</param>
        /// <param name="other">the second supplied collection of sets</param>
        /// <returns></returns>
        public static Tuple<Tuple<Set<T>, Set<T>>, double> Closest<T>(this IEnumerable<Set<T>> set, IEnumerable<Set<T>> other)
            where T : IMetric<T>
        {
            return set.Minimizers(other, (s1, s2) => s1.Distance(s2));
        }
    }

    public static class EnumerableMetricExtensions
    {
        public static SimplicialComplex<T> ToSimpleGraph<T>(this IEnumerable<T> metricSpace, double threshold)
            where T : IMetric<T>
        {
            var edges = new Set<Set<T>>();
            var enumerable = metricSpace as T[] ?? metricSpace.ToArray();
            foreach (var item1 in enumerable)
            {
                var item3 = item1;
                foreach (var item2 in enumerable.Where(item2 => item3.Distance(item2) <= threshold))
                    edges.Add(new Set<T> { item1, item2 });
            }
            return new SimplicialComplex<T>(edges);
        }
    }
}
