using System.Linq;
using CSharpExtensions.ContainerClasses;
using MathsCore.LinearAlgebra;

namespace MathsCore
{
    public class Polynomial<TField> : Vector<int, TField>
    {
        public override string ToString()
        {
            var result = "";
            var degree = Degree;
            Keys.OrderBy(key => -key).Each(key =>
            {
                if (this[key] > 0 && key != degree)
                    result += "+";
                result += (this[key] == 1 && key != 0 ? string.Empty : this[key]) + PowerString(key);
            });
            return result;
        }

        public Polynomial()
        {
        }

        private Polynomial(Vector<int, TField> vector)
        {
            foreach (var key in vector.Keys)
                this[key] = vector[key];
        }

        public static Polynomial<TField> operator +(Polynomial<TField> p1, Polynomial<TField> p2)
        {
            return new Polynomial<TField>(p1 + (Vector<int, TField>)p2);
        }

        public static Polynomial<TField> operator *(Polynomial<TField> p1, Polynomial<TField> p2)
        {
            var result = new Polynomial<TField>();
            foreach (var key in p1.Keys)
            {
                var intermediate = new Polynomial<TField>();
                var key1 = key;
                p2.EachKey(key2 => intermediate[key1 + key2] = p1[key1] * p2[key2]);
                result += intermediate;
            }
            return result;
        }

        public Polynomial<TField> Differentiate()
        {
            var result = new Polynomial<TField>();
            foreach (var key in Keys)
                result[key - 1] = key * this[key];

            return result;
        }

        public Polynomial<TField> Integrate()
        {
            var result = new Polynomial<TField>();
            foreach (var key in Keys.Where(key => key != -1))
                result[key + 1] = this[key] / (key + 1);
            // note integral not always a polynomial because integral of x^{-1} is log
            return result;
        }

        private static string PowerString(int n)
        {
            if (n == 1)
                return "x";
            if (n == 0)
                return string.Empty;
            return "x^" + n;
        }

        public int Degree
        {
            get { return Keys.Max(); }
        }
    }
}