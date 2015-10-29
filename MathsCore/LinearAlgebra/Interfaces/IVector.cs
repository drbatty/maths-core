using System.Collections.Generic;
using MathsCore.Interfaces;

namespace MathsCore.LinearAlgebra.Interfaces
{
    public interface IVector<TBasis, TField> : IDictionary<TBasis, TField>, IMetric<IVector<TBasis, TField>>
    {
        IVector<TBasis, TField> Normalize();
        dynamic Dot(IVector<TBasis, TField> other);
        bool IsZero();
    }
}