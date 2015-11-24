using System;
using System.Collections.Generic;
using CSharpExtensionsTests;
using MathsCore.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.LinearAlgebra
{
    [TestClass]
    public class MatrixExtensionsTests
    {
        private static Vector<Tuple<int, int>, int> A
        {
            get
            {
                return new Vector<Tuple<int, int>, int>
                {
                    {new Tuple<int, int>(0, 0), 1},
                    {new Tuple<int, int>(0, 1), 2},
                    {new Tuple<int, int>(1, 0), 0},
                    {new Tuple<int, int>(1, 1), 1}
                };
            }
        }

        [TestMethod]
        public void Matrix_multiplication_should_give_correct_results()
        {
            var b = new Vector<Tuple<int, int>, int>
            {
                {new Tuple<int, int>(0, 0), 1},
                {new Tuple<int, int>(0, 1), 0},
                {new Tuple<int, int>(1, 0), 2},
                {new Tuple<int, int>(1, 1), 1}
            };

            var c = new Vector<Tuple<int, int>, int>
            {
                {new Tuple<int, int>(0, 0), 5},
                {new Tuple<int, int>(0, 1), 2},
                {new Tuple<int, int>(1, 0), 2},
                {new Tuple<int, int>(1, 1), 1}
            };

            A.Product(b).ShouldEqual(c);
        }

        [TestMethod]
        public void Matrix_identity_should_be_correct_for_2_by_2()
        {
            var indices = new List<int> {1, 2};
            var id = MatrixExtensions.Identity<int, int>(indices);
            id.Count.ShouldEqual(2); // only the diagonal entries are nonzero
            Assert.AreEqual(id[new Tuple<int, int>(1, 1)], 1);
            Assert.AreEqual(id[new Tuple<int, int>(2, 2)], 1);
        }

        [TestMethod]
        public void Transpose_should_be_correct_for_2_by_2()
        {
            var transpose = A.Transpose();
            transpose.GetEntry(0, 0).ShouldEqual(1);
            transpose.ContainsKey(new Tuple<int, int>(0, 1)).ShouldBeFalse();
            transpose.GetEntry(1, 0).ShouldEqual(2);
            transpose.GetEntry(1, 1).ShouldEqual(1);
        }

        [TestMethod]
        public void RowKeys_and_Column_Keys_should_be_correct()
        {
            var b = new Vector<Tuple<int, int>, int>
            {
                {new Tuple<int, int>(0, 0), 1},
                {new Tuple<int, int>(2, 1), -1},
                {new Tuple<int, int>(0, 1), 2},
                {new Tuple<int, int>(0, 2), 1}
            };
            b.ColumnKeys().ShouldNumber(3);
            b.RowKeys().ShouldNumber(2);
            b.Width().ShouldEqual(3);
            b.Height().ShouldEqual(3);
        }

        [TestMethod]
        public void Product_of_matrix_and_vector_should_be_correct()
        {
            var matrix = new Vector<Tuple<int, int>, int>
            {
                {new Tuple<int, int>(0, 0), 1},
                {new Tuple<int, int>(0, 1), 2},
                {new Tuple<int, int>(1, 0), 3},
                {new Tuple<int, int>(1, 1), 4}
            };

            var vector = new Vector<int, int>();
            vector[0] = 1;
            vector[1] = 1;

            var product = matrix.Product(vector);
            product.Keys.ShouldNumber(2);
            Assert.AreEqual(product[0], 3);
            Assert.AreEqual(product[1], 7);
        }

        [TestMethod]
        public void TestMatrixPower()
        {
            var b = new Vector<Tuple<int, int>, int>
            {
                {new Tuple<int, int>(0, 0), 1},
                {new Tuple<int, int>(1, 0), 1},
                {new Tuple<int, int>(1, 1), 1}
            };

            b.Power(2).ShouldEqual(
                new Vector<Tuple<int, int>, int>
            {
                {new Tuple<int, int>(0, 0), 1},
                {new Tuple<int, int>(1, 0), 2},
                {new Tuple<int, int>(1, 1), 1}
            }
                );
        }

        [TestMethod]
        public void TestSetEntry()
        {
            var matrix = new Vector<Tuple<int, int>, int>();
            matrix.SetEntry(1, 1, 5);
            matrix.GetEntry(1, 1).ShouldEqual(5);  
        }

        [TestMethod]
        public void TestMatrixDisplay()
        {
            var matrix = new Vector<Tuple<int, int>, int>();
            matrix.Display().ShouldEqual("");
            var matrix2 = new Vector<Tuple<int, int>, int>
            {
                {new Tuple<int, int>(0, 0), 1},
                {new Tuple<int, int>(0, 1), 2},
                {new Tuple<int, int>(1, 0), 3},
                {new Tuple<int, int>(1, 1), 4}
            };
            matrix2.Display().ShouldEqual("1 2 " + Environment.NewLine + "3 4 " + Environment.NewLine);
        }
    }
}