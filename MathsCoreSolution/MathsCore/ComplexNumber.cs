using CSharpExtensions;
using CSharpExtensions.Text;
using MathsCore.LinearAlgebra;

namespace MathsCore
{
    public class ComplexNumber : Vector2D
    {
        public ComplexNumber(double x, double y)
            : base(x, y)
        {
        }

        public ComplexNumber(Vector2D v)
            : base(v.X, v.Y)
        {
        }

        public ComplexNumber(Vector<string, double> v)
            : base((double)v["x"], (double)v["y"])
        {
        }

        public double ModulusSquared()
        {
            return X * X + Y * Y;
        }

        public double Re
        {
            get { return X; }
        }

        public double Im
        {
            get { return Y; }
        }

        #region equality operators

        public override bool Equals(object other)
        {
            return !other.Isnt<ComplexNumber>() && Equals((ComplexNumber)other);
        }

        protected bool Equals(ComplexNumber other)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            return Re == other.Re && Im == other.Im;
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }

        public override int GetHashCode()
        {
            return Re.GetHashCode() ^ Im.GetHashCode();
        }
        //
        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Equals(b);
        }

        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !(a == b);
        }

        #endregion

        #region arithmetic operations

        public static ComplexNumber operator +(ComplexNumber z1, ComplexNumber z2)
        {
            return new ComplexNumber(z1 + (Vector2D)z2);
        }

        public static ComplexNumber operator *(ComplexNumber z1, ComplexNumber z2)
        {
            return z1.Product(z2);
        }

        public ComplexNumber Product(ComplexNumber w)
        {
            return new ComplexNumber(X * w.X - Y * w.Y,
                X * w.Y + Y * w.X);
        }

        #endregion

        public override string ToString()
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            var result = Re == 0 ? string.Empty : Re.ToS();
            if (Im > 0 & Re != 0)
                result += "+";
            result += Im == 0 ? string.Empty :
                (Im == 1 ? string.Empty :
                (Im == -1 ? "-" : Im.ToS())
                ) + "i";
            // ReSharper restore CompareOfFloatsByEqualityOperator
            return result;
        }
    }
}