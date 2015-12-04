namespace MathsCore.NumberCrunching
{
    public class QuakeReciprocalSquareRoot
    {
        // NB can use for vector normalization 

        public static float Q_rsqrt(float number)
        {
            float y;
            const float threehalfs = 1.5F;

            var x2 = number * 0.5F;
            y = number;
            var i = (long)y;
            i = 0x5f3759df - (i >> 1);
            y = i;
            y = y * (threehalfs - (x2 * y * y)); // 1st iteration
            //      y  = y * ( threehalfs - ( x2 * y * y ) );   // 2nd iteration, this can be removed

            return y;
        }
    }
}