using System.Linq.Expressions;

namespace MathsCore.SymbolicManipulation
{
    public static class AlgebraicExtensions
    {
        public static Expression Differentiate(this Expression source)
        {
            return new DifferentiationVisitor().Visit(source);
        }
    }
}