using System.Collections.Generic;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Sets;

namespace MathsCore.Graph.Directed.Interfaces
{
    public interface IDirectedEdge<out TVertexType>
    {
        TVertexType InitialVertex { get; }
        TVertexType TerminalVertex { get; }
    }

    public static class DirectedEdgeExtensions
    {
        public static bool AdjacentTo<TVertexType>(this IDirectedEdge<TVertexType> edge, TVertexType v)
        {
            return edge.InitialVertex.Equals(v) || edge.TerminalVertex.Equals(v);
        }

        public static Set<TVertexType> Vertices<TVertexType>(this IDirectedEdge<TVertexType> edge)
        {
            return new List<TVertexType> { edge.InitialVertex, edge.TerminalVertex }.ToSet();
        }
    }
}
