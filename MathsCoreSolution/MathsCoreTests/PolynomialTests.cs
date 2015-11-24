using CSharpExtensionsTests;
using MathsCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests
{
    [TestClass]
    public class PolynomialTests
    {
        [TestMethod]
        public void Polynomial_product_should_be_correct()
        {
            var p = new Polynomial<double>();
            p[1] = 1;
            p[0] = 2;

            var p2 = new Polynomial<double>();
            p2[1] = 1;
            p2[0] = 3;

            var product = p * p2;
            Assert.IsTrue(product[2] == 1);
            Assert.IsTrue(product[1] == 5);
            Assert.IsTrue(product[0] == 6);
        }

        [TestMethod]
        public void Polynomial_ToString_should_be_correct()
        {
            var p = new Polynomial<double>();
            p[0] = 1;
            p.ToString().ShouldEqual("1");
            p[0] = -1;
            p[1] = 1;
            p.ToString().ShouldEqual("x-1");
            p[0] = 1;
            p.ToString().ShouldEqual("x+1");
            p[2] = 1;
            p.ToString().ShouldEqual("x^2+x+1");
        }

        [TestMethod]
        public void Polynomial_differentation_should_be_correct()
        {
            var p2 = new Polynomial<double>();
            p2[2] = 3;
            p2[1] = 1;
            p2[0] = 3;
            p2[-2] = 2;
            var p = p2.Differentiate();
            Assert.IsTrue(p[1] == 6);
            Assert.IsTrue(p[0] == 1);
            Assert.IsTrue(p[-3] == -4);
            p.Keys.ShouldNumber(3);
        }

        [TestMethod]
        public void Polynomial_integration_should_be_correct()
        {
            var p2 = new Polynomial<double>();
            p2[2] = 3;
            p2[1] = 1;
            p2[0] = 3;
            p2[-2] = 2;
            var p = p2.Integrate();
            Assert.IsTrue(p[3] == 1);
            Assert.IsTrue(p[2] == 0.5);
            Assert.IsTrue(p[1] == 3);
            Assert.IsTrue(p[-1] == -2);
            p.Keys.ShouldNumber(4);
        }
    }
}
