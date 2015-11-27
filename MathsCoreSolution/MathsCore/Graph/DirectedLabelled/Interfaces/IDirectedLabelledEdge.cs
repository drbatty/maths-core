using MathsCore.Graph.Directed.Interfaces;

namespace MathsCore.Graph.DirectedLabelled.Interfaces
{
    public interface IDirectedLabelledEdge<out TVertexType, TLabelType> : IDirectedEdge<TVertexType>
    {
        TLabelType Label { get; set; }
    }
}