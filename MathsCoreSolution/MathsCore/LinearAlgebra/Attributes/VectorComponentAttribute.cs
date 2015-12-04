using System;

namespace MathsCore.LinearAlgebra.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class VectorComponentAttribute : Attribute
    {
    }
}
