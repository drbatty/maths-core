using MathsCore;
using MathsCore.LinearAlgebra;
using MathsCore.LinearAlgebra.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.LinearAlgebra
{
    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void UnitShouldBeDefinedForRecognizedTypes()
        {
            Assert.IsTrue(Field.Unit(typeof(int)) == 1);
            Assert.IsTrue(Field.Unit(typeof(double)) == 1.0);
            Assert.IsTrue(Field.Unit(typeof(ComplexNumber)) == new ComplexNumber(1, 0));
        }

        [TestMethod]
        public void ZeroShouldBeDefinedForRecognizedTypes()
        {
            Assert.IsTrue(Field.Zero(typeof(int)) == 0);
            Assert.IsTrue(Field.Zero(typeof(double)) == 0.0);
            Assert.IsTrue(Field.Zero(typeof(ComplexNumber)) == new ComplexNumber(0 , 0));
        }

        [TestMethod]
        [ExpectedException(typeof (UnrecognisedFieldException))]
        public void UnrecognizedFieldExceptionShouldOccurForUnitOfUnknownFieldTypes()
        {
            Field.Unit(typeof (string));
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end

        [TestMethod]
        [ExpectedException(typeof(UnrecognisedFieldException))]
        public void UnrecognizedFieldExceptionShouldOccurForZeroOfUnknownFieldTypes()
        {
            Field.Zero(typeof(string));
            //ncrunch: no coverage start
        }
        //ncrunch: no coverage end
    }
}
