using CSharpExtensionsTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Sets
{
    [TestClass]
    public class MetricIntegerTests
    {
        [TestMethod]
        public void TestInequalityDifferentType()
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            new MetricInteger(1).Equals("").ShouldBeFalse();
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod]
        public void TestStandardEquality()
        {
            new MetricInteger(1).ShouldEqual(new MetricInteger(1));
        }

        [TestMethod]
        public void TestHashCode()
        {
            new MetricInteger(1).GetHashCode().ShouldEqual(1.GetHashCode());
        }
    }
}