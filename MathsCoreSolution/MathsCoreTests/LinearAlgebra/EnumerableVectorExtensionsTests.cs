using System.Collections.Generic;
using CSharpExtensionsTests;
using MathsCore.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.LinearAlgebra
{
    [TestClass]
    public class EnumerableVectorExtensionsTests
    {
        [TestMethod]
        public void ToVector_should_give_correct_answer()
        {
            var enumerable = new List<string> { "a", "b", "a", "c", "c", "c" };
            var vector = enumerable.ToVector();
            vector.Keys.ShouldNumber(3);
            Assert.IsTrue(vector["a"] == 2);
            Assert.IsTrue(vector["b"] == 1);
            Assert.IsTrue(vector["c"] == 3);
        }
    }
}