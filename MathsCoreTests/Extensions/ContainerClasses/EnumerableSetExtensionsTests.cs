using CSharpExtensions;
using CSharpExtensionsTests;
using MathsCore.Extensions.ContainerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Extensions.ContainerClasses
{
    [TestClass]
    public class EnumerableSetExtensionsTests
    {
        #region ToSet tests

        [TestMethod]
        public void ToSetTest2()
        {
            (1.Upto(3).ToSet()).ShouldContainExactly(1, 2, 3);
        }

        #endregion
    }
}