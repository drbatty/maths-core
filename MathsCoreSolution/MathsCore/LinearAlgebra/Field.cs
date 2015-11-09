using System;
using MathsCore.LinearAlgebra.Exceptions;

namespace MathsCore.LinearAlgebra
{
    public static class Field
    {
        public static dynamic Unit(Type type)
        {
            if (type == typeof(int))
                return 1;
            if (type == typeof(double))
                return 1.0;
            if (type == typeof(ComplexNumber))
                return new ComplexNumber(1, 0);
            throw new UnrecognisedFieldException(type);
        }

        public static dynamic Zero(Type type)
        {
            if (type == typeof(int))
                return 0;
            if (type == typeof(double))
                return 0.0;
            if (type == typeof(ComplexNumber))
                return new ComplexNumber(0, 0);
            throw new UnrecognisedFieldException(type);
        }
    }
}