using System;

namespace MathsCore.Graph.Exceptions
{
    public class NonexistentVertexException<T> : Exception
    {
        private readonly T _vertex;

        public NonexistentVertexException(T vertex)
        {
            _vertex = vertex;
        }

        public override string Message
        {
            get { return "The specified vertex " + _vertex + " is not a vertex of this graph. "; }
        }
    }
}