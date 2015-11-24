using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Sets;

namespace MathsCore.LinearAlgebra
{
    public static class MatrixExtensions
    {
        #region entry getters and setters

        public static TField GetEntry<TBasis, TField>(this IDictionary<Tuple<TBasis, TBasis>, TField> matrix, TBasis i, TBasis j)
        {
            return matrix[new Tuple<TBasis, TBasis>(i, j)];
        }

        public static void SetEntry<TBasis, TField>(this IDictionary<Tuple<TBasis, TBasis>, TField> matrix, TBasis i,
                                                    TBasis j, TField value)
        {
            matrix[new Tuple<TBasis, TBasis>(i, j)] = value;
        }

        #endregion

        #region assignation helper methods

        private static Vector<Tuple<TBasis, TBasis>, TField> Assign<TBasis, TField>(IEnumerable<TBasis> basis, Func<TBasis, TBasis, dynamic> assignFunc)
        {
            return new Vector<Tuple<TBasis, TBasis>, TField>().Return(v => basis.EachPair((t, u) => v[new Tuple<TBasis, TBasis>(t, u)] = assignFunc(t, u)));
        }

        private static Vector<TBasis, TField> Assign<TBasis, TField>(IEnumerable<TBasis> basis, Func<TBasis, dynamic> assignFunc)
        {
            return new Vector<TBasis, TField>().Return(v => basis.Each(b => v[b] = assignFunc(b)));
        }

        private static Vector<Tuple<TBasis, TBasis>, TField> Assign<TBasis, TField>(this IDictionary<Tuple<TBasis, TBasis>, TField> matrix, Func<TBasis, TBasis, dynamic> assignFunc)
        {
            return new Vector<Tuple<TBasis, TBasis>, TField>().Return(v =>
                matrix.RowKeys().Union(matrix.ColumnKeys())
                    .EachPair((i, j) => v[new Tuple<TBasis, TBasis>(i, j)] = assignFunc(i, j)));
        }

        #endregion

        public static Vector<Tuple<TBasis, TBasis>, TField> Identity<TBasis, TField>(IEnumerable<TBasis> basis)
        {
            var returnValue = Assign<TBasis, TField>(basis, (t, u) => t.Equals(u) ? Field.Unit(typeof(TField)) : Field.Zero(typeof(TField)));
            return returnValue;
        }

        public static Vector<Tuple<TBasis, TBasis>, TField> Power<TBasis, TField>(this Vector<Tuple<TBasis, TBasis>, TField> matrix, int power)
        {
            return 1.Upto(power).Inject(Identity<TBasis, TField>(matrix.RowKeys().Union(matrix.ColumnKeys())), (acc, i) => acc.Product(matrix));
        }

        public static Vector<Tuple<TBasis, TBasis>, TField> Product<TBasis, TField>(this Vector<Tuple<TBasis, TBasis>, TField> matrix1, Vector<Tuple<TBasis, TBasis>, TField> matrix2)
        {
            return Assign<TBasis, TField>(matrix1.RowKeys().Intersect(matrix2.ColumnKeys()),
                (k1, k2) => new Vector<TBasis, TField>(matrix1.Row(k1)).Dot(new Vector<TBasis, TField>(matrix2.Column(k2))));
        }

        public static Vector<TBasis, TField> Product<TBasis, TField>(this Vector<Tuple<TBasis, TBasis>, TField> matrix, Vector<TBasis, TField> vector)
        {
            return Assign<TBasis, TField>(matrix.RowKeys().Intersect(vector.Keys), key => new Vector<TBasis, TField>(matrix.Row(key)).Dot(vector));
        }

        public static Vector<TBasis, TField> Row<TBasis, TField>(this IDictionary<Tuple<TBasis, TBasis>, TField> matrix, TBasis t)
        {
            return Assign<TBasis, TField>(matrix.Keys.Where(key => key.Item1.Equals(t)).Select(k => k.Item2), item2 => matrix.GetEntry(t, item2));
        }

        public static Vector<TBasis, TField> Column<TBasis, TField>(this IDictionary<Tuple<TBasis, TBasis>, TField> matrix, TBasis t)
        {
            return Assign<TBasis, TField>(matrix.Keys.Where(key => key.Item2.Equals(t)).Select(k => k.Item1), item1 => matrix.GetEntry(item1, t));
        }

        public static Set<TBasis> RowKeys<TBasis, TField>(this IDictionary<Tuple<TBasis, TBasis>, TField> matrix)
        {
            return matrix.Keys.ToList().Select(key => key.Item1).ToSet();
        }

        public static Set<TBasis> ColumnKeys<TBasis, TField>(this IDictionary<Tuple<TBasis, TBasis>, TField> matrix)
        {
            return matrix.Keys.ToList().Select(key => key.Item2).ToSet();
        }

        public static Vector<Tuple<TBasis, TBasis>, TField> Transpose<TBasis, TField>(
            this Vector<Tuple<TBasis, TBasis>, TField> matrix)
        {
            return matrix.Assign((u, v) => matrix[new Tuple<TBasis, TBasis>(v, u)]);
        }

        public static int MinColumnKey<TField>(this IDictionary<Tuple<int, int>, TField> matrix)
        {
            return matrix.ColumnKeys().Min();
        }

        public static int MaxColumnKey<TField>(this IDictionary<Tuple<int, int>, TField> matrix)
        {
            return matrix.ColumnKeys().Max();
        }

        public static int MinRowKey<TField>(this IDictionary<Tuple<int, int>, TField> matrix)
        {
            return matrix.RowKeys().Min();
        }

        public static int MaxRowKey<TField>(this IDictionary<Tuple<int, int>, TField> matrix)
        {
            return matrix.RowKeys().Max();
        }

        public static int Width<TField>(this IDictionary<Tuple<int, int>, TField> matrix)
        {
            return matrix.MaxColumnKey() - matrix.MinColumnKey() + 1;
        }

        public static int Height<TField>(this IDictionary<Tuple<int, int>, TField> matrix)
        {
            return matrix.MaxRowKey() - matrix.MinRowKey() + 1;
        }

        public static string Display<TBasis, TField>(this IDictionary<Tuple<TBasis, TBasis>, TField> matrix)
        {
            var result = string.Empty;
            var labels = matrix.RowKeys().Union(matrix.ColumnKeys()).OrderBy(k => k);
            labels.Each(l =>
            {
                labels.Each(m => result += (matrix.ContainsKey(new Tuple<TBasis, TBasis>(l, m)) ? matrix.GetEntry(l, m).ToString() : "-" ) + " ");
                result += Environment.NewLine;
            });
            return result;
        }

        public static bool IsSymmetric<TBasis, TField>(this Vector<Tuple<TBasis, TBasis>, TField> matrix)
        {
            return matrix.Equals(matrix.Transpose());
        }
    }
}