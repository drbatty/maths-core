using System;

namespace MathsCore.SimplicialComplex.Exceptions
{
    public class ComplexesAreNotDisjointException : Exception
    {
        public override string ToString()
        {
            return "Factors of disjoint union are not disjoint";
        }
    }
}