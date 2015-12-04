using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Graph.Directed.Interfaces;
using MathsCore.Graph.Exceptions;
using MathsCore.Sets;

namespace MathsCore.Graph.Directed
{
    public class DirectedGraph<TVertexType> : IDirectedGraph<TVertexType>
    {
        #region IGraph implementation

        public Set<TVertexType> Vertices { get; set; }
        public Set<IDirectedEdge<TVertexType>> Edges { get; set; }

        #endregion

        #region constructors

        public DirectedGraph()
        {
            Vertices = new Set<TVertexType>();
            Edges = new Set<IDirectedEdge<TVertexType>>();
        }

        public DirectedGraph(Set<TVertexType> vertices, Set<IDirectedEdge<TVertexType>> edges)
        {
            Vertices = vertices;
            Edges = edges;
        }

        #endregion constructor

        public bool HasEdge(TVertexType vertex1, TVertexType vertex2)
        {
            return Edges.Contains(new DirectedEdge<TVertexType>(vertex1, vertex2));
        }

        public void RemoveVertex(TVertexType t)
        {
            if (!(t < Vertices))
                throw new NonexistentVertexException<TVertexType>(t);
            Vertices.Remove(t);
            Edges.Where(e => e.AdjacentTo(t)).Each(e => Edges.Remove(e));
        }

        public bool AreAdjacent(TVertexType t1, TVertexType t2)
        {
            if (!(t1 < Vertices))
                throw new NonexistentVertexException<TVertexType>(t1);
            if (!(t2 < Vertices))
                throw new NonexistentVertexException<TVertexType>(t2);
            return Edges.Any(e => e.AdjacentTo(t1) && e.AdjacentTo(t2));
        }

        public IEnumerable<TVertexType> AdjacentVertices(TVertexType t)
        {
            if (!(t < Vertices))
                throw new NonexistentVertexException<TVertexType>(t);
            return Vertices.Where(v => AreAdjacent(v, t) && !v.Equals(t));
        }

        public IEnumerable<TVertexType> AdjacentVertices(Set<TVertexType> set)
        {
            var nonExistent = Vertices.WhereNot(v => v < Vertices);
            var enumerable = nonExistent as IList<TVertexType> ?? nonExistent.ToList();
            if (enumerable.Any())
                throw new NonexistentVertexException<TVertexType>(enumerable.FirstOrDefault());
            return set.Select(s => AdjacentVertices(s).ToSet()).Union();
        }

        public IEnumerable<IDirectedEdge<TVertexType>> AdjacentEdges(TVertexType t)
        {
            if (!(t < Vertices))
                throw new NonexistentVertexException<TVertexType>(t);
            return Edges.Where(e => e.AdjacentTo(t));
        }

        public IEnumerable<IDirectedEdge<TVertexType>> AdjacentEdges(Set<TVertexType> set)
        {
            var nonExistent = Vertices.WhereNot(v => v < Vertices);
            var enumerable = nonExistent as IList<TVertexType> ?? nonExistent.ToList();
            if (enumerable.Any())
                throw new NonexistentVertexException<TVertexType>(enumerable.FirstOrDefault());
            return set.Select(s => AdjacentEdges(s).ToSet()).Union();
        }

        public DirectedGraph<TVertexType> InducedSubgraph(Set<TVertexType> vertices)
        {
            var nonExistent = Vertices.WhereNot(v => v < Vertices);
            var enumerable = nonExistent as IList<TVertexType> ?? nonExistent.ToList();
            if (enumerable.Any())
                throw new NonexistentVertexException<TVertexType>(enumerable.FirstOrDefault());
            return new DirectedGraph<TVertexType>(vertices, new Set<IDirectedEdge<TVertexType>>(AdjacentEdges(vertices).ToSet()));
        }

        public IDirectedGraph<TVertexType> RestrictToInitialVertex(TVertexType t)
        {
            return new DirectedGraph<TVertexType>
            {
                Vertices = Vertices,
                Edges = new Set<IDirectedEdge<TVertexType>>(Edges.Where(e => ((DirectedEdge<TVertexType>)e).InitialVertex.Equals(t)).ToSet())
            };
        }

        public IDirectedGraph<TVertexType> RestrictToFinalVertex(TVertexType t)
        {
            return new DirectedGraph<TVertexType>
            {
                Vertices = Vertices,
                Edges = new Set<IDirectedEdge<TVertexType>>(Edges.Where(e => ((DirectedEdge<TVertexType>)e).TerminalVertex.Equals(t)).ToSet())
            };
        }

        public List<TVertexType> Star(List<TVertexType> vertices)
        {
            var result = vertices.ToList();
            foreach (var vertex in Vertices)
            {
                var vertex1 = vertex;
                Vertices.Each(v =>
                {
                    if (Edges.Contains(new DirectedEdge<TVertexType>(v, vertex1)) & !result.Contains(vertex1))
                        result.Add(v);
                });
            }
            return result;
        }

        public List<TVertexType> Component(TVertexType vertex, int maxTimes)
        {
            var result = new List<TVertexType> { vertex };
            maxTimes.Times(() => result = Star(result));
            return result.Return(r => r.Sort());
        }

        public List<TVertexType> Component(TVertexType vertex)
        {
            return Component(vertex, Vertices.Count);
        }

        public int ComponentSize(TVertexType vertex)
        {
            return Component(vertex).Count();
        }

        public override string ToString()
        {
            return Edges.Aggregate("", (current, iE) => current + (iE.ToString() + "\n"));
        }
    }
}