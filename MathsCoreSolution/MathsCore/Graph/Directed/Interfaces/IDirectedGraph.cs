using System.Linq;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Sets;

namespace MathsCore.Graph.Directed.Interfaces
{
    public interface IDirectedGraph<TVertexType>
    {
        Set<TVertexType> Vertices { get; set; }
        Set<IDirectedEdge<TVertexType>> Edges { get; set; }
        IDirectedGraph<TVertexType> RestrictToInitialVertex(TVertexType t);
        IDirectedGraph<TVertexType> RestrictToFinalVertex(TVertexType t);
    }

    public static class DirectedGraphExtensions
    {
        public static Set<IDirectedEdge<T>> InEdges<T>(this IDirectedGraph<T> directedGraph, T vertex)
        {
            return directedGraph.RestrictToFinalVertex(vertex).Edges;
        }

        public static Set<IDirectedEdge<T>> OutEdges<T>(this IDirectedGraph<T> directedGraph, T vertex)
        {
            return directedGraph.RestrictToInitialVertex(vertex).Edges;
        }

        public static Set<T> InNeighbours<T>(this IDirectedGraph<T> directedGraph, T vertex)
        {
            return directedGraph.Edges.Where(e => e.TerminalVertex.Equals(vertex)).Select(e => e.InitialVertex).ToSet();
        }

        public static Set<T> OutNeighbours<T>(this IDirectedGraph<T> directedGraph, T vertex)
        {
            return directedGraph.Edges.Where(e => e.InitialVertex.Equals(vertex)).Select(e => e.TerminalVertex).ToSet();
        }

        public static int InRank<T>(this IDirectedGraph<T> directedGraph, T vertex)
        {
            return directedGraph.InEdges(vertex).Count();
        }

        public static int OutRank<T>(this IDirectedGraph<T> directedGraph, T vertex)
        {
            return directedGraph.OutEdges(vertex).Count();
        }

        public static bool HasInEdges<T>(this IDirectedGraph<T> directedGraph, T vertex)
        {
            return directedGraph.InRank(vertex) > 0;
        }

        public static bool HasOutEdges<T>(this IDirectedGraph<T> directedGraph, T vertex)
        {
            return directedGraph.OutRank(vertex) > 0;
        }

        public static int Order<T>(this IDirectedGraph<T> directedGraph)
        {
            return directedGraph.Vertices.Count();
        }

        public static int Size<T>(this IDirectedGraph<T> directedGraph)
        {
            return directedGraph.Edges.Count();
        }
    }
}