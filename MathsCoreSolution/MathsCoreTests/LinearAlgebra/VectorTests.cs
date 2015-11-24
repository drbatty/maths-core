using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensionsTests;
using MathsCore.Interfaces;
using MathsCore.LinearAlgebra;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.LinearAlgebra
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void TestVectorStringDoubleAddition()
        {
            var v1 = new Vector<string, double> { { "x", 1 } };
            var v2 = new Vector<string, double> { { "y", 1 } };
            var v = v1 + v2;
            double d = v["x"];
            d.ShouldEqual(1);
            d = v["y"];
            d.ShouldEqual(1);
            IEnumerable<string> keys = v.Keys;
            keys.ShouldNumber(2);
        }

        [TestMethod]
        public void TestVectorsAddToZero()
        {
            var v1 = new Vector<string, double> { { "x", 1 } };
            var v2 = new Vector<string, double> { { "x", -1 } };
            (v1 + v2).IsZero().ShouldBeTrue();
        }

        [TestMethod]
        public void TestVectorMultiplication()
        {
            var v1 = new Vector<string, double> { { "x", 1 }, { "y", -2 } };
            var v = 2.0 * v1;
            double d = v["x"];
            d.ShouldEqual(2);
            d = v["y"];
            d.ShouldEqual(-4);
            IEnumerable<string> keys = v.Keys;
            keys.ShouldNumber(2);
        }

        [TestMethod]
        public void TestIntVectorAddition()
        {
            var v1 = new Vector<string, int> { { "x", 1 } };
            var v2 = new Vector<string, int> { { "y", 1 } };
            var v = v1 + v2;
            double d = v["x"];
            d.ShouldEqual(1);
            d = v["y"];
            d.ShouldEqual(1);
            IEnumerable<string> keys = v.Keys;
            keys.ShouldNumber(2);
        }

        [TestMethod]
        public void TestIntVectorMultiplication()
        {
            var v1 = new Vector<string, int> { { "x", 1 }, { "y", -2 } };
            var v = 2 * v1;
            double d = v["x"];
            d.ShouldEqual(2);
            d = v["y"];
            d.ShouldEqual(-4);
            IEnumerable<string> keys = v.Keys;
            keys.ShouldNumber(2);
        }

        [TestMethod]
        public void TestEmptyVectorIsZero()
        {
            var v = new Vector<string, int>();
            v.IsZero().ShouldBeTrue();
        }

        [TestMethod]
        public void TestIntVectorSubtraction()
        {
            var v1 = new Vector<string, int> { { "x", 1 } };
            var v2 = new Vector<string, int> { { "y", 1 } };
            var v = v1 - v2;
            double d = v["x"];
            d.ShouldEqual(1);
            d = v["y"];
            d.ShouldEqual(-1);
            IEnumerable<string> keys = v.Keys;
            keys.ShouldNumber(2);
        }

        [TestMethod]
        public void TestSparseness()
        {
            var v = new Vector<string, int>();
            v["x"] = 0;
            v.IsZero().ShouldBeTrue();
        }

        [TestMethod]
        public void TestSparseness2()
        {
            var v = new Vector<string, int>();
            int i = v["x"];
            i.ShouldEqual(0);
        }

        [TestMethod]
        public void TestZeroMultiplication()
        {
            var v1 = new Vector<string, int> { { "x", 1 }, { "y", -2 } };
            var v = 0 * v1;
            v.IsZero().ShouldBeTrue();
        }

        [TestMethod]
        public void TestVectorHashcode()
        {
            var v = new Vector<string, int> { { "x", 1 }, { "y", -2 } };
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            v.GetHashCode();
        }

        #region equality and inequality tests

        [TestMethod]
        public void TestIntVectorEquality()
        {
            var v1 = new Vector<string, int> { { "x", 1 }, { "y", -2 } };
            var v2 = new Vector<string, int> { { "x", 1 }, { "y", -2 } };
            v1.Equals(v2).ShouldBeTrue();
        }

        [TestMethod]
        public void TestInequalityWrongType()
        {
            var v1 = new Vector<string, int>();
            // ReSharper disable once SuspiciousTypeConversion.Global
            v1.Equals("").ShouldBeFalse();
        }

        [TestMethod]
        public void TestSparseEquality()
        {
            var v1 = new Vector<string, int> { { "x", 1 }, { "y", 0 } };
            var v2 = new Vector<string, int> { { "x", 1 }};
            v1.Equals(v2).ShouldBeTrue();
        }

        #endregion

        #region IDictionary implementation tests

        [TestMethod]
        public void TestGetEnumerator()
        {
            new Vector<string, int>().GetEnumerator();
        }

        [TestMethod]
        public void TestContains()
        {
            new Vector<string, int>().Contains(new KeyValuePair<string, int>("a",1)).ShouldBeFalse();
            new Vector<string, int>{{"a", 1}}.ShouldContain(new KeyValuePair<string, int>("a", 1));
        }

        [TestMethod]
        public void TestClear()
        {
            new Vector<string, int> {{"a", 1}}.Return(v => v.Clear())
                .Contains(new KeyValuePair<string, int>("a", 1)).ShouldBeFalse();
        }

        [TestMethod]
        public void TestAdd()
        {
            new Vector<string, int>().Return(v => v.Add(new KeyValuePair<string, int>("a", 1))).ShouldContain(new KeyValuePair<string, int>("a", 1));
            new Vector<string, int>().Return(v => v.Add(new KeyValuePair<string, int>("a", 0))).Contains(new KeyValuePair<string, int>("a", 0)).ShouldBeFalse();
        }

        [TestMethod]
        public void TestRemove()
        {
            new Vector<string, int> { { "a", 1 } }.Return(v => v.Remove(new KeyValuePair<string, int>("a", 1)))
                .Contains(new KeyValuePair<string, int>("a", 1)).ShouldBeFalse();
        }

        #endregion

        [TestMethod]
        public void TestDotProductOrthogonal()
        {
            var v1 = new Vector<string, double> { { "x", 1 } };
            var v2 = new Vector<string, double> { { "y", 1 } };
            double d = v1.Dot(v2);
            d.ShouldEqual(0);
        }

        [TestMethod]
        public void TestDotProductNonOrthogonal()
        {
            var v1 = new Vector<string, int> { { "x", 1 }, { "y", 2 } };
            var v2 = new Vector<string, int> { { "x", 3 }, { "y", -4 } };
            int d = v1.Dot(v2);
            d.ShouldEqual(-5);
        }

        [TestMethod]
        public void TestIntModulus()
        {
            var v = new Vector<string, int> { { "x", 3 }, { "y", 4 } };
            var d = v.Modulus();
            d.ShouldEqual(5.0);
        }

        [TestMethod]
        public void TestZeroVectorModulus()
        {
            var v = new Vector<string, int>();
            var m = v.Modulus();
            m.ShouldEqual(0.0);
        }

        [TestMethod]
        public void TestNormalizeZero()
        {
            var v = new Vector<string, int>();
            v.Normalize().IsZero().ShouldBeTrue();
        }

        [TestMethod]
        public void TestNormalizeNonZero()
        {
            var v = new Vector<string, double> { { "x", 1 } };
            v.Normalize().Equals(v).ShouldBeTrue();
        }

        [TestMethod]
        public void TestSelfDistance()
        {
            var v = new Vector<string, double> { { "x", 1 } };
            v.Distance(v).ShouldEqual(0);
        }

        [TestMethod]
        public void TestOrthogonalDistance()
        {
            var v1 = new Vector<string, double> { { "x", 1 } };
            var v2 = new Vector<string, double> { { "y", 1 } };
            v1.Distance(v2).ShouldEqual(1.0);
        }

        [TestMethod]
        public void TestOppositeDistance()
        {
            var v1 = new Vector<string, double> { { "x", 1 } };
            var v2 = new Vector<string, double> { { "x", -1 } };
            v1.Distance(v2).ShouldEqual(2.0);
        }

        [TestMethod]
        public void TestClosest()
        {
            var list = new List<Vector<string, double>>
            {
                new Vector<string, double> {{"x", 1}},
                new Vector<string, double> {{"y", 1}}
            };

            var v = new Vector<string, double> {{"x", 2}, {"y", 1}};
            var closest = v.Closest(new Set<Vector<string, double>>(list));
            closest.Equals(list.First()).ShouldBeTrue();
        }

        [TestMethod]
        public void TestClosestNull()
        {
            var list = new List<Vector<string, double>>();
            var v = new Vector<string, double> { { "x", 2 }, { "y", 1 } };
            Assert.IsTrue(v.Closest(new Set<Vector<string, double>>(list)) == null);
        }

        [TestMethod]
        public void TestTruncate()
        {
            var v = new Vector<string, int> { {"x", 2}, {"y", 1}, {"z", 3} };
            v.Truncate(2).Keys.ShouldEqual("x", "z");
        }

        [TestMethod]
        public void TestSingletonConstructor()
        {
            var v = new Vector<string, int>("a");
            v.Keys.Count.ShouldEqual(1);
            Assert.IsTrue(v["a"] == 1);
        }

        [TestMethod]
        public void TestPointwise()
        {
            Func<int, int, int> func = (x, y) => x * y;
            var v = new Vector<string, int> { { "x", 2 }, { "y", 1 } };
            var w = new Vector<string, int> { { "x", 3 }, { "y", -3 } };
            v.PointWise(w, func).ShouldEqual(new Vector<string, int> { { "x", 6 }, { "y", -3 } });
        }

        [TestMethod]
        public void TestDiceCoefficient()
        {
            var v = new Vector<string, double> { { "x", 1 }, { "y", 1 } };
            var w = new Vector<string, double> { { "x", 1 }, { "y", 2 } };
            Assert.IsTrue(v.DiceCoefficient(w) - 0.857142857142857 < 0.0000000000001);
        }

        [TestMethod]
        public void TestToString()
        {
            var v = new Vector<string, double> {{"x", 1}, {"y", 2}};
            v.ToString().ShouldEqual("2|y> 1|x> ");
        }
        /*
            AddTest(new InputOutputTest<IntVector<string>, List<Vector<string, int>>>
            {
                Description = "test of proper factors of a vector",

                CreateInput = () => new IntVector<string> { { "x", 6 }, { "y", 4 } },

                GetOutput = input => input.ProperFactors(new List<string> { "x", "y" }),

                Expectation = output => output.Count > 0
            });
         */
    }
}
