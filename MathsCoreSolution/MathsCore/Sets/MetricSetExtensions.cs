using System;
using System.Linq;
using CSharpExtensions.ContainerClasses;
using MathsCore.Interfaces;

namespace MathsCore.Sets
{
    /// <summary>
    /// Extensions methods for the generic set class which require a concept of distance on the set
    /// </summary>
    public static class MetricSetExtensions
    {
        /// <summary>
        /// for a metric set, returns the distance between a generic element and the set
        /// </summary>
        /// <typeparam name="T">the generic parameter for the set, which must extend the type IMetric in itself</typeparam>
        /// <param name="t">generic argument</param>
        /// <param name="set">the set to calculate distence from </param>
        /// <returns>for a metric set, the distance between a generic element and the set</returns>
        public static double Distance<T>(this T t, Set<T> set)
            where T : IMetric<T>
        {
            if (set.None())
                throw new Exception("set can't be empty");
            return set.Min(e2 => t.Distance(e2));
        }

        /// <summary>
        /// returns the distance between a metric set and another supplied metric set
        /// </summary>
        /// <typeparam name="T">the type of object the set contains, which must extend the type implement IMetric in itself</typeparam>
        /// <param name="set">the operand set</param>
        /// <param name="other">the other supplied metric set</param>
        /// <returns>the distance between a metric set and another supplied metric set</returns>
        public static double Distance<T>(this Set<T> set, Set<T> other)
            where T : IMetric<T>
        {
            if (set.None() || other.None())
                throw new Exception("neither set can be empty");
            return set.Min(t => t.Distance(other));
        }
    }
}