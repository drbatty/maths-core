using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Graph.Exceptions;
using MathsCore.Interfaces;
using MathsCore.Sets;
using MathsCore.SimplicialComplex.Exceptions;

namespace MathsCore.SimplicialComplex
{
    public class SimplicialComplex<TVertexType> : Set<Set<TVertexType>>, IRecognisable
    {
        public string Name { get; set; }

        public SimplicialComplex(IEnumerable<Set<TVertexType>> setSet)
            : this()
        {
            setSet.Each(Add);
        }

        public SimplicialComplex()
        {
            Clear();
        }

        public override void Add(Set<TVertexType> set)
        {
            if (!Elements.Contains(set))
                Elements.Add(set);
            if (set.Count <= 1) return;
            set.Each(element => Add(set.Exclude(element).ToSet()));
        }

        public SimplicialComplex<TVertexType> Star(TVertexType vertex)
        {
            if (!this.Vertices().Contains(vertex))
                throw new NonexistentVertexException<TVertexType>(vertex);
            return new SimplicialComplex<TVertexType>(this.Where(s => s.Contains(vertex)));
        }

        public int Degree(TVertexType vertex)
        {
            return Star(vertex).Edges().Count;
        }

        public Set<TVertexType> Neighbours(TVertexType vertex)
        {
            return Star(vertex).Skeleton(1).SelectMany(v => v).ToSet();
        }

        public IEnumerable<int> DegreeSequence()
        {
            return this.Vertices().Select(Degree).OrderBy(i => i);
        }

        public IEnumerable<TVertexType> DistanceSort(IEnumerable<TVertexType> basepoints)
            // only depends on the 1-skeleton so can be a simplicial complex method
        {
            var result = new List<TVertexType>();
            var boundary = new List<TVertexType>();
            var vertexTypes = basepoints as IList<TVertexType> ?? basepoints.ToList();
            if (vertexTypes.Any(v => !this.Vertices().Contains(v)))
                throw new NonexistentVertexException<TVertexType>(vertexTypes.First(v => !this.Vertices().Contains(v)));
            vertexTypes.Each(v =>
            {
                result.Add(v);
                boundary.Add(v);
            });
            while (boundary.Any())
            {
                var newBoundary = new List<TVertexType>();
                boundary.Each(v => Neighbours(v).ToList().Each(w =>
                {
                    if (result.Contains(w)) return;
                    result.Add(w);
                    newBoundary.Add(w);
                }));
                boundary = newBoundary;
            }
            // add vertices not in any of the connected components containing the vertices in the argument.
            this.Vertices().Each(v =>
            {
                if (!result.Contains(v))
                    result.Add(v);
            });
            return result;
        }

        public bool IsFace(Set<TVertexType> set)
        {
            return this.Any(s => set < s);
        }

        public Set<Set<TVertexType>> NonFaces
        {
            get { return this.Where(set => !IsFace(set)).ToSet(); }
        }

        public int Dimension
        {
            get { return this.Any() ? this.Max(set => set.Count) - 1 : -1; }
        }

        public string Recognise()
        {
            if (this.None())
                return "The empty complex";
            if (this.IsCompleteGraph())
                return "Complete graph of order " + this.Order();
            if (Dimension == 0)
                return "Totally disconnected graph on " + this.Order() + (this.Order() == 1 ? " vertex" : " vertices");
            //ncrunch: no coverage start
            return string.Empty;
            //ncrunch: no coverage end
        }

        public override string ToString()
            // omits all faces as they are contained by definition, so only shows maximial elements
        {
            return "{" + NonFaces.CommaSeparate("Δ") + "}";
        }

        public SimplicialComplex<TVertexType> DisjointUnion(SimplicialComplex<TVertexType> other)
        {
            if (this.Vertices().Meets(other.Vertices()))
                throw new ComplexesAreNotDisjointException();
            return new SimplicialComplex<TVertexType>(this + other);
        }

        public SimplicialComplex<Tuple<TVertexType, TOtherVertexType>> GraphProduct<TOtherVertexType>(
            SimplicialComplex<TOtherVertexType> other,
            Func<Tuple<TVertexType, TOtherVertexType>, Tuple<TVertexType, TOtherVertexType>, bool> predicate)
        {
            var result = new SimplicialComplex<Tuple<TVertexType, TOtherVertexType>>();
            foreach (var pair in this.Vertices().CartesianProduct(other.Vertices()))
                result.Add(new Set<Tuple<TVertexType, TOtherVertexType>> {pair});
            result.AddRange(result.Vertices()
                .CartesianProduct(result.Vertices())
                .Where(t => predicate(t.Item1, t.Item2))
                .Select(t => new Set<Tuple<TVertexType, TOtherVertexType>> {t.Item1, t.Item2}));
            return result;
        }

        public SimplicialComplex<Tuple<TVertexType, TOtherVertexType>> GraphCartesianProduct<TOtherVertexType>(
            SimplicialComplex<TOtherVertexType> other)
        {
            return GraphProduct(other,
                (v, w) => v.Item1.Equals(w.Item1) && other.Contains(new Set<TOtherVertexType> {v.Item2, w.Item2})
                          || (Contains(new Set<TVertexType> {v.Item1, w.Item1}) && v.Item2.Equals(w.Item2)));
        }

        public SimplicialComplex<Tuple<TVertexType, TOtherVertexType>> GraphTensorProduct<TOtherVertexType>(
            SimplicialComplex<TOtherVertexType> other)
        {
            return GraphProduct(other, (v, w) => Contains(new Set<TVertexType> {v.Item1, w.Item1}) &&
                                                 other.Contains(new Set<TOtherVertexType> {v.Item2, w.Item2}));
        }

        public SimplicialComplex<Tuple<TVertexType, string>> BipartiteDoubleCover()
        {
            return GraphTensorProduct(SimpleGraphExtensions.CompleteGraph(2));
        }

        public SimplicialComplex<TVertexType> InducedSubcomplex(Set<TVertexType> vertices)
        {
            return new SimplicialComplex<TVertexType>(this.Where(set => set <= vertices));
        }

        public SimplicialComplex<TVertexType> FlagComplex()
        {
            var result = new SimplicialComplex<TVertexType>(this);
            var extraSimplices = new Set<Set<TVertexType>>();
            do
            {
                result.AddRange(extraSimplices);
                extraSimplices = new Set<Set<TVertexType>>();
                result.Each(set => result.Vertices().Each(v =>
                {
                    var vset = new Set<TVertexType> {v};
                    if (vset <= set) return;
                    var simplex = set | vset;
                    if (!result.Contains(simplex) &&
                        simplex.All(vertex => result.Contains(simplex.Exclude(vertex).ToSet())))
                        extraSimplices.Add(simplex);
                }));
            } while (extraSimplices.Any());
            return result;
        }
    }
}