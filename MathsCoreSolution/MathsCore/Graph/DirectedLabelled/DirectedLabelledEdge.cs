using System.Collections.Generic;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Graph.DirectedLabelled.Interfaces;
using MathsCore.Sets;

namespace MathsCore.Graph.DirectedLabelled
{
    public class DirectedLabelledEdge<TVertex, TLabel> : IDirectedLabelledEdge<TVertex, TLabel>
    {
        #region IDirectedEdge<TVertex> implementation

        #region IEdge<TVertex> implementation

        public bool AdjacentTo(TVertex t)
        {
            return InitialVertex.Equals(t) || TerminalVertex.Equals(t);
        }

        public Set<TVertex> Vertices
        {
            get { return new List<TVertex> { InitialVertex, TerminalVertex }.ToSet(); }
        }

        #endregion

        public TVertex InitialVertex { get; set; }
        public TVertex TerminalVertex { get; set; }

        #endregion

        #region IDirectedLabelledEdge<TVertex, TLabel> implementation

        public TLabel Label { get; set; }

        #endregion
    }
}