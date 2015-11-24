using System;
using MathsCore.Interfaces;

namespace MathsCoreTests.Sets
{
    class MetricInteger : IMetric<MetricInteger>
    {
        private int Int { get; set; }

        public MetricInteger(int pInt)
        {
            Int = pInt;
        }

        public double Distance(MetricInteger other)
        {
            return Math.Abs(Int - other.Int);
        }

        #region

        public override bool Equals(object other)
        {
            return (other is MetricInteger) && Equals((MetricInteger)other);
        }

        protected bool Equals(MetricInteger other)
        {
            return Int == other.Int;
        }

        public override int GetHashCode()
        {
            return Int.GetHashCode();
        }

        #endregion
    }
}