using System;
using MathsCore.Graph.Directed.Interfaces;

namespace MathsCore.Graph.Directed
{
    public class DirectedEdge<TVertexType> : Tuple<TVertexType, TVertexType>, IDirectedEdge<TVertexType>
    {
        public DirectedEdge(TVertexType item1, TVertexType item2)
            : base(item1, item2)
        {
        }

        #region IDirectedEdge<T> implementation

        public TVertexType InitialVertex
        {
            get { return Item1; }
        }

        public TVertexType TerminalVertex
        {
            get { return Item2; }
        }

        #endregion
    }
}