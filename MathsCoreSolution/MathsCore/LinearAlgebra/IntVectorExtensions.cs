using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions;

namespace MathsCore.LinearAlgebra
{
    public static class IntVectorExtensions
    {
        public static List<Vector<T, int>> ProperFactors<T>(this Vector<T, int> vector, IEnumerable<T> basis)
        {
            var result = new List<Vector<T, int>>();
            var factors = new List<List<int>>();
            if (basis == null)
                return result;
            var enumerable = basis.ToList();
            enumerable.Each(basisElement => factors.Add(NumberTheoreticIntExtensions.ProperFactors(Math.Abs(vector[basisElement]))));
            factors.Intersection().Each(factor => result.Add(CreateVector(enumerable, n => Math.Sign(n) * (n / factor))));
            return result;
        }

        private static Vector<T, int> CreateVector<T>(this IEnumerable<T> basis, Func<int, int> lambda)
        {
            var result = new Vector<T, int>();
            basis.Each(key => result[key] = lambda(result[key]));
            return result;
        }

        public static Vector<string, int> TwoDimensional(int x, int y)
        {
            return new Vector<string, int> { { "x", x }, { "y", y } };
        }

        public static int X<T>(this T vector)
            where T : Vector<string, int>
        {
            return vector.GetOrDefault("x");
        }

        public static int Y<T>(this T vector)
            where T : Vector<string, int>
        {
            return vector.GetOrDefault("y");
        }

        public static bool Undoubleable<T>(this T vector)
            where T : Vector<string, int>
        {
            return vector.X() == 0 || vector.Y() == 0 || vector.X() == vector.Y();
        }

        public static bool IsTwoDimensionalMultipleOf<T>(this T vector, T other)
            where T : Vector<string, int>
        {
            var x = vector.X();
            var y = vector.Y();
            if (other.IsZero())
                return vector.IsZero();
            if (other.X() == 0)
            {
                if (x == 0)
                    return other.Y().Divides(y) && y / other.Y() >= 0;
                return false;
            }
            if (other.Y() != 0)
                return other.X().Divides(x) && other.Y().Divides(y) && x / other.X() == y / other.Y() && x / other.X() >= 0;
            if (y == 0)
                return other.X().Divides(x) && x / other.X() >= 0;
            return false;
        }

        public static int TwoDimensionalQuotient<T>(this T vector, T other)
            where T : Vector<string, int>
        {
            if (!vector.IsTwoDimensionalMultipleOf(other))
                return 0;
            if (other.X() != 0)
                return vector.X() / other.X();
            if (other.Y() != 0)
                return vector.Y() / other.Y();
            return 0;
        }
    }
}
