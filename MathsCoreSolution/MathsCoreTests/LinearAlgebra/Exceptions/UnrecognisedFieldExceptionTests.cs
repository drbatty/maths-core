using CSharpExtensionsTests;
using MathsCore.LinearAlgebra.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.LinearAlgebra.Exceptions
{
    [TestClass]
    public class UnrecognisedFieldExceptionTests
    {
        [TestMethod]
        public void TestUnrecognisedFieldException()
        {
            new UnrecognisedFieldException(typeof(string)).ToString().ShouldEqual("the type " + typeof(string) + " is not a recognized mathematical field with a defined zero and unit.");
        }
    }
}