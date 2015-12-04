using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Graph.Directed;
using MathsCore.Graph.Directed.Interfaces;
using MathsCore.Graph.DirectedLabelled.Interfaces;
using MathsCore.Sets;

namespace MathsCore.Graph.DirectedLabelled
{
    /// <summary>
    /// generic class to represent a directed labelled graph. The out edges for a vertex are of the form
    /// of a dictionary of keys of type TVertex, and whose values are of the form of dictionary
    /// from TEdges to TVertices, to allow for multiple labelled edges out of a vertex
    /// </summary>
    /// <typeparam name="TVertexType">the type of the vertices</typeparam>
    /// <typeparam name="TLabelType">the type of the labels</typeparam>
    public class DirectedLabelledGraph<TVertexType, TLabelType> : IDirectedLabelledGraph<TVertexType, TLabelType>
    {
        /// <summary>
        /// the set of vertices of the directed labelled graph
        /// </summary>
        public Set<TVertexType> Vertices { get; set; }

        /// <summary>
        /// the set of edges of the directed labelled graph
        /// </summary>
        public Set<IDirectedEdge<TVertexType>> Edges { get; set; }

        public DirectedLabelledGraph(Set<TVertexType> vertices,
            Set<IDirectedEdge<TVertexType>> edges)
        {
            Vertices = vertices;
            Edges = edges;
        }

        public DirectedLabelledGraph()
        {
            Vertices = new Set<TVertexType>();
            Edges = new Set<IDirectedEdge<TVertexType>>();
        }

        public void AddVertex(TVertexType vertex)
        {
            Vertices.Add(vertex);
        }

        public bool HasOutEdge(TVertexType vertex, IDirectedEdge<TVertexType> edge)
        {
            return this.HasOutEdges(vertex) && RestrictToInitialVertex(vertex).Edges.Contains(edge);
        }

        public bool HasOutEdge(TVertexType vertex, TLabelType label)
        {
            return this.HasOutEdges(vertex) &&
                   RestrictToInitialVertex(vertex)
                       .Edges.Any(e => ((DirectedLabelledEdge<TVertexType, TLabelType>) e).Label.Equals(label));
        }

        public Set<TLabelType> Labels
        {
            get { return Edges.Select(e => ((DirectedLabelledEdge<TVertexType, TLabelType>) e).Label).ToSet(); }
        }

        public Set<TLabelType> InLabels(TVertexType vertex)
        {
            return
                this.InEdges(vertex)
                    .Cast<DirectedLabelledEdge<TVertexType, TLabelType>>()
                    .Select(e => e.Label)
                    .ToSet();
        }

        public Set<TLabelType> OutLabels(TVertexType vertex)
        {
            return
                this.OutEdges(vertex)
                    .Cast<DirectedLabelledEdge<TVertexType, TLabelType>>()
                    .Select(e => e.Label)
                    .ToSet();
        }

        public Set<TLabelType> AllLabels(TVertexType vertex)
        {
            return InLabels(vertex) | OutLabels(vertex);
        }

        public TVertexType TerminalVertex(TVertexType vertex, IDirectedEdge<TVertexType> edge)
        {
            return HasOutEdge(vertex, edge)
                ? RestrictToInitialVertex(vertex).Edges.First(e => e.Equals(edge)).TerminalVertex
                : default(TVertexType);
        }

        public TVertexType TerminalVertex(TVertexType vertex, TLabelType label)
        {
            return HasOutEdge(vertex, label)
                ? RestrictToInitialVertex(vertex).Edges.First
                    (e => ((DirectedLabelledEdge<TVertexType, TLabelType>) e).Label.Equals(label)).TerminalVertex
                : default(TVertexType);
        }

        public Set<IDirectedEdge<TVertexType>> DirectedEdges { get; set; }

        public IDirectedGraph<TVertexType> RestrictToInitialVertex(TVertexType t)
        {
            var edges = Edges.Where(e => ((DirectedLabelledEdge<TVertexType, TLabelType>) e).InitialVertex.Equals(t));
            var edgeSet = new Set<IDirectedEdge<TVertexType>>();
            edgeSet.AddRange(edges);
            return new DirectedGraph<TVertexType>(t.WrapInList().ToSet(), edgeSet);
        }

        public IDirectedGraph<TVertexType> RestrictToFinalVertex(TVertexType t)
        {
            var edges = Edges.Where(e => ((DirectedLabelledEdge<TVertexType, TLabelType>)e).TerminalVertex.Equals(t));
            var edgeSet = new Set<IDirectedEdge<TVertexType>>();
            edgeSet.AddRange(edges);
            return new DirectedGraph<TVertexType>(t.WrapInList().ToSet(), edgeSet);
        }
    }
}