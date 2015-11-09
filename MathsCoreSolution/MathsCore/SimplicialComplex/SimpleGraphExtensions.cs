using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Text;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.LinearAlgebra;
using MathsCore.Sets;

namespace MathsCore.SimplicialComplex
{
    public static class SimpleGraphExtensions
    {
        #region private methods

        private static string VertexNumber(int i)
        {
            return "v_" + i.ToS();
        }

        #endregion

        #region static methods for producing graphs

        #region predicate graphs

        public static SimplicialComplex<string> PredicateGraph(int n, Func<int, int, bool> predicate)
        {
            return new SimplicialComplex<string>(1.Upto(n).Select(i => new Set<string> { VertexNumber(i) })).Return(s =>
                1.Upto(n).EachPair((i, j) => predicate(i, j).If(() => s.Add(new Set<string> { VertexNumber(i), VertexNumber(j) }))));
        }

        public static SimplicialComplex<string> CompleteGraph(int n)
        {
            return PredicateGraph(n, (i, j) => true).Return(g => g.Name = "Complete graph of order " + n);
        }

        public static SimplicialComplex<string> EdgelessGraph(int n)
        {
            return PredicateGraph(n, (i, j) => false)
                    .Return(g => g.Name = "Totally disconnected graph on " + n + "vertices");
        }

        public static SimplicialComplex<string> PathGraph(int n)
        {
            return PredicateGraph(n, (i, j) => j == i + 1).Return(g => g.Name = "Path Graph P_" + n);
        }

        public static SimplicialComplex<string> CycleGraph(int n)
        {
            return PredicateGraph(n, (i, j) => j % n == (i + 1) % n).Return(g => g.Name = "Cycle Graph C_" + n);
        }

        public static SimplicialComplex<string> RandomGraph(int n, double edgeProbability) // todo generalise to random complex
        {
            var random = new Random();
            return PredicateGraph(n, (i, j) => random.NextDouble() < edgeProbability);
        }

        public static SimplicialComplex<string> CompleteBipartiteGraph(int n, int m)
        {
            return PredicateGraph(n + m, (i, j) => i <= n && j > n);
        }

        public static SimplicialComplex<string> StarGraph(int n)
        {
            return CompleteBipartiteGraph(1, n);
        }

        #endregion

        #region cartesian products

        public static SimplicialComplex<Tuple<string, string>> GridGraph(int n, int m)
        {
            return PathGraph(n).GraphCartesianProduct(PathGraph(m));
        }

        public static SimplicialComplex<Tuple<string, string>> LadderGraph(int n, int m)
        {
            return GridGraph(n, 2);
        }

        public static SimplicialComplex<Tuple<string, string>> PrismGraph(int n, int m)
        {
            return CycleGraph(n).GraphCartesianProduct(PathGraph(m));
        }

        public static SimplicialComplex<Tuple<string, string>> StackedBookGraph(int n, int m)
        {
            return StarGraph(n).GraphCartesianProduct(PathGraph(m));
        }

        public static SimplicialComplex<Tuple<string, string>> BookGraph(int n)
        {
            return StackedBookGraph(n, 2);
        }

        #endregion

        #endregion

        #region extension methods

        public static bool IsCompleteGraph<TVertexType>(this SimplicialComplex<TVertexType> complex)
        {
            if (complex.Dimension <= 0 && complex.Order() <= 1)
                return true;
            if (complex.Dimension != 1)
                return false;
            return complex.Edges().Count() == (complex.Order() * (complex.Order() - 1)) / 2;
        }

        public static SimplicialComplex<TVertexType> Complement<TVertexType>(this SimplicialComplex<TVertexType> complex)
        {
            var result = new SimplicialComplex<TVertexType>(complex.Vertices().Singletons());
            complex.Vertices().EachPair((v, w) => new Set<TVertexType> { v, w }.Return(s =>
                complex.Contains(s).IfNot(() => result.Add(s))));
            return result;
        }

        public static SimplicialComplex<Set<string>> KneserGraph(int order, int m)
        {
            return 1.Upto(order).Select(VertexNumber).ToSet().KneserSet(m).IntersectionGraph().Complement();
        }

        public static SimplicialComplex<Set<string>> PetersenGraph()
        {
            return KneserGraph(5, 2);
        }

        public static SimplicialComplex<Set<TVertexType>> CliqueGraph<TVertexType>(this SimplicialComplex<TVertexType> complex)
        {
            return complex.FlagComplex().NonFaces.Where(set => complex.InducedSubcomplex(set).IsCompleteGraph()).IntersectionGraph();
        }

        #region algebraic

        public static IDictionary<Tuple<TVertexType, TVertexType>, TField> AdjacencyMatrix<TField, TVertexType>(
            this SimplicialComplex<TVertexType> complex)
        {
            var result = new Dictionary<Tuple<TVertexType, TVertexType>, TField>();

            complex.Vertices()
                .EachPair((v, w) => result[new Tuple<TVertexType, TVertexType>(v, w)] = Field.Zero(typeof(TField)));

            complex.Edges().Each(e =>
            {
                result[new Tuple<TVertexType, TVertexType>(e.ElementAt(0), e.ElementAt(1))] = Field.Unit(typeof(TField));
                result[new Tuple<TVertexType, TVertexType>(e.ElementAt(1), e.ElementAt(0))] = Field.Unit(typeof(TField));
            });

            return result;
        }

        #endregion

        #endregion
    }
}
