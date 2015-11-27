using MathsCore.Graph.Directed.Interfaces;

namespace MathsCore.Graph.DirectedLabelled.Interfaces
{
    public interface IDirectedLabelledGraph<TVertexType, in TLabelType> : IDirectedGraph<TVertexType>
    {
        bool HasOutEdge(TVertexType vertex, TLabelType label);
        TVertexType TerminalVertex(TVertexType vertex, TLabelType label);
    }
}