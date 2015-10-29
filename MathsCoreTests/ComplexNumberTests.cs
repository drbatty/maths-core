using CSharpExtensionsTests;
using MathsCore;
using MathsCore.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void ToString_should_produce_natural_results()
        {
            new ComplexNumber(1, 0).ToString().ShouldEqual("1");
            new ComplexNumber(1, 1).ToString().ShouldEqual("1+i");
            new ComplexNumber(1, 2).ToString().ShouldEqual("1+2i");
            new ComplexNumber(1, -1).ToString().ShouldEqual("1-i");
            new ComplexNumber(0, 1).ToString().ShouldEqual("i");
            new ComplexNumber(0, 2).ToString().ShouldEqual("2i");
            new ComplexNumber(1, -2).ToString().ShouldEqual("1-2i");
            new ComplexNumber(0, -1).ToString().ShouldEqual("-i");
            new ComplexNumber(-1, -1).ToString().ShouldEqual("-1-i");
        }

        [TestMethod]
        public void Multiplication_should_produce_correct_results()
        {
            (new ComplexNumber(1, 3) * new ComplexNumber(-1, 1)).ShouldEqual(new ComplexNumber(-4, -2));
        }

        [TestMethod]
        public void Addition_should_produce_correct_results()
        {
            (new ComplexNumber(1, 3) + new ComplexNumber(-1, 1)).ShouldEqual(new ComplexNumber(0, 4));
        }

        [TestMethod]
        public void Modulus_squared_should_produce_correct_results()
        {
            (new ComplexNumber(1, 3)).ModulusSquared().ShouldEqual(10);
        }

        [TestMethod]
        public void Should_construct_from_vector2D()
        {
            new ComplexNumber(new Vector2D(1, 2)).ShouldEqual(new ComplexNumber(1, 2));
        }
    }
}