using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;

namespace MathsCore.Extensions
{
    public static class NumberTheoreticIntExtensions
    {
        public static bool Divides(this int i, int j)
        {
            return i != 0 && j % i == 0;
        }

        public static List<int> ProperFactors(this int i)
        {
            return 2.Upto(i - 2).Where(j => j.Divides(i)).ToList();
        }

        public static List<int> PrimeFactorisation(this int i)
        {
            var remainder = i;
            var result = new List<int>();
            while (remainder > 1)
            {
                var factor = 2;
                while (!factor.Divides(remainder))
                    factor++;
                remainder /= factor;
                result.Add(factor);
            }
            return result;
        }

        public static IEnumerable<Tuple<int, int>> PrimeFactorisationWithPowers(this int i)
        {
            return i.PrimeFactorisation().Frequencies();
        }

        public static int F(this int i)
        {
            return i.PrimeFactorisation().Sum();
        }

        public static int NumberOfDivisors(this int i)
        {
            return i.WithPrimeFactorisation(pair => pair.Item2 + 1); // uses theorem which reduces to multiplying the powers (+1) in a prime factorisation
        }

        // ReSharper disable once InconsistentNaming
        public static int τ(this int i)
        {
            return i.NumberOfDivisors();
        }

        public static bool IsPerfect(this int i)
        {
            return i.Deficiency() == 0;
        }

        public static int Deficiency(this int i)
        {
            return i - i.AliquotSum();
        }

        public static int Abundance(this int i)
        {
            return -i.Deficiency();
        }

        public static bool IsDeficient(this int i)
        {
            return i.Deficiency() > 0;
        }

        public static bool IsAbundant(this int i)
        {
            return i.Abundance() > 0;
        }

        public static int AliquotSum(this int i)
        {
            return i.σ() - i; // nb more efficient to use σ which has a product expression.
        }

        // ReSharper disable once InconsistentNaming
        public static int σ(this int i)
        {
            return i.WithPrimeFactorisation(pair => ((int)Math.Pow(pair.Item1, pair.Item2 + 1) - 1) / (pair.Item1 - 1));
        }

        public static List<int> AliquotSequence(this int i, int length)
        {
            var result = new List<int>();
            var current = i;
            length.Do(j =>
            {
                result.Add(current);
                current = AliquotSum(current);
            });
            return result;
        }

        public static List<int> ModularAliquotSequence(this int i, int length, int modulus)
        {
            var result = new List<int>();
            var current = i % modulus;
            length.Do(j =>
            {
                result.Add(current);
                current = AliquotSum(current) % modulus;
            });
            return result;
        }

        public static List<int> ModularAliquotSequenceLengths(this int modulus, int max)
        {
            var result = new List<int>();
            modulus.Do(j =>
            {
                var length = 0;
                var current = j % modulus;
                while (length < max && current != 0)
                {
                    current = AliquotSum(current) % modulus;
                    length++;
                }
                result.Add(length);
            });
            return result;
        }

        public static string ToBinary(this int n)
        {
            var b = new char[32];
            var pos = 31;
            var i = 0;

            while (i < 32)
            {
                if ((n & (1 << i)) != 0)
                    b[pos] = '1';
                else
                    b[pos] = '0';
                pos--;
                i++;
            }
            return new string(b);
        }

        public static int Factorial(this int n)
        {
            if (n <= 1)
                return 1;
            return n * (n - 1).Factorial();
        }

        public static bool IsPrime(this int number)
        {
            if (number % 2 == 0)
                return number == 2;

            var sqrt = (int)Math.Sqrt(number);
            for (var t = 3; t <= sqrt; t = t + 2)
                if (number % t == 0)
                    return false;

            return number != 1;
        }

        public static List<int> PrimesUpTo(this int i)
        {
            return 1.Upto(i).Where(j => j.IsPrime()).ToList();
        }

        public static int GreatestCommonDivisor(this int a, int b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static int LeastCommonMultiple(this int a, int b)
        {
            return (a / a.GreatestCommonDivisor(b)) * b;
        }

        private static int WithPrimeFactorisation(this int i, Func<Tuple<int, int>, int> λ)
        {
            return i.PrimeFactorisationWithPowers().Product(λ);
        }

        // ReSharper disable once InconsistentNaming
        public static int ϕ(this int i) // Euler phi function
        {
            return i.WithPrimeFactorisation(pair => (int)Math.Pow(pair.Item1, pair.Item2) - (int)Math.Pow(pair.Item1, pair.Item2 - 1));
        }
    }
}
