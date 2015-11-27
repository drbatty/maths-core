using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;

namespace MathsCore.Extensions.ContainerClasses
{
    public static class EnumerableMathsExtensions
    {
        /// <summary>
        /// Computes all the combinations of elements from a given IEnumerable
        /// </summary>
        /// <typeparam name="T">the type which the IEnumerable enumerates</typeparam>
        /// <param name="source">the IEnumerable which forms the enumerable of the combinations</param>
        /// <param name="select">the number of elements to select</param>
        /// <param name="repetition">whether or not repetition is allowed</param>
        /// <returns>an IEnumerable of enumerables, which enumerates the combinations of the given IEnumerable</returns>
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int select,
            bool repetition = false)
        {
            var enumerable = source as IList<T> ?? source.ToList();
            return select == 0
                ? new[] { new T[0] }
                : enumerable.SelectMany((element, index) => enumerable
                    .Skip(repetition ? index : index + 1)
                    .Combinations(select - 1, repetition)
                    .Select(c => new[] { element }.Concat(c)));
        }

        /// <summary>
        /// returns the value in an IEnumerable which minimizes a double-valued function of the IEnumerable
        /// </summary>
        /// <typeparam name="T">the type of object enumerated by the IEnumerable</typeparam>
        /// <param name="iEnumerable">the IEnumerable to minimize over</param>
        /// <param name="λ">the function to minimize</param>
        /// <returns>the value in an IEnumerable which minimizes the given double-valued function of the IEnumerable</returns>
        public static Tuple<T, double> Minimizer<T>(this IEnumerable<T> iEnumerable, Func<T, double> λ)
        {
            var enumerable = iEnumerable as IList<T> ?? iEnumerable.ToList();
            if (enumerable.None() || λ == null)
                return null;
            var min = enumerable.Min(λ);
            return new Tuple<T, double>(enumerable.FirstOrDefault(v => λ(v) == min), min);
        }

        /// <summary>
        /// returns the value in an IEnumerable which maximizes a double-valued function of the IEnumerable
        /// </summary>
        /// <typeparam name="T">the type of object enumerated by the IEnumerable</typeparam>
        /// <param name="iEnumerable">the IEnumerable to maximize over</param>
        /// <param name="λ">the function to maximize</param>
        /// <returns>the value in an IEnumerable which maximizes the given double-valued function of the IEnumerable</returns>
        public static Tuple<T, double> Maximizer<T>(this IEnumerable<T> iEnumerable, Func<T, double> λ)
        {
            var enumerable = iEnumerable as IList<T> ?? iEnumerable.ToList();
            if (enumerable.None() || λ == null)
                return null;
            var max = enumerable.Max(λ);
            return new Tuple<T, double>(enumerable.FirstOrDefault(v => λ(v) == max), max);
        }

        /// <summary>
        /// the minimum over a pair of enumerables
        /// </summary>
        /// <typeparam name="T">the type of object enumerated by the IEnumerable</typeparam>
        /// <param name="iEnumerable">the operand IEnumerable</param>
        /// <param name="other">the enumerable2 given IEnumerable</param>
        /// <param name="λ">the function to minimize</param>
        /// <returns>the minimum over the given pair of enumerables</returns>
        public static double Min<T>(this IEnumerable<T> iEnumerable, IEnumerable<T> other,
            Func<T, T, double> λ)
        {
            var enumerable = iEnumerable as IList<T> ?? iEnumerable.ToList();
            var ss = other as IList<T> ?? other.ToList();
            if (enumerable.None() || ss.None() || λ == null)
                return default(double);
            return enumerable.Min(s1 => ss.Min(s2 => λ(s1, s2)));
        }

        /// <summary>
        /// returns the pair of values which minimize a function on a pair of enumerables, and the minimum value
        /// </summary>
        /// <typeparam name="T">the type enumerated by the IEnumerable</typeparam>
        /// <param name="iEnumerable">the operand IEnumerable</param>
        /// <param name="other">the enumerable2 IEnumerable</param>
        /// <param name="lambda">the function to minimize</param>
        /// <returns>the pair of values which minimize a function on a pair of enumerables, and the minimum value</returns>
        public static Tuple<Tuple<T, T>, double> Minimizers<T>(this IEnumerable<T> iEnumerable, IEnumerable<T> other,
            Func<T, T, double> λ)
        {
            var enumerable = iEnumerable as IList<T> ?? iEnumerable.ToList();
            var ss = other as IList<T> ?? other.ToList();
            if (enumerable.None() || ss.None() || λ == null)
                return null;
            var minimizers = enumerable.Select(t1 => t1.Pair(ss.Minimizer(t2 => λ(t1, t2)).Item1));
            var minimizer = minimizers.Minimizer(tuple => λ(tuple.Item1, tuple.Item2));
            return minimizer.Item1.Pair(minimizer.Item2);
        }

        public static int Frequency<T>(this IEnumerable<T> enumerable, T t)
        {
            return enumerable.Count(i => i.Equals(t));
        }

        public static IEnumerable<Tuple<T, int>> Frequencies<T>(this IEnumerable<T> enumerable)
        {
            var e = enumerable as IList<T> ?? enumerable.ToList();
            return e.Distinct().Select(t => t.Pair(e.Frequency(t)));
        }
    }
}
