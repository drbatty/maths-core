namespace MathsCore.LinearAlgebra
{
    public class Vector2D : Vector<string, double>
    {
        public Vector2D(double x, double y)
        {
            this["x"] = x;
            this["y"] = y;
        }

        public double X
        {
            get { return this["x"]; }
        }

        public double Y
        {
            get { return this["y"]; }
        }
    }
}