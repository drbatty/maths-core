using System.Collections.Generic;
using System.Collections.ObjectModel;
using CSharpExtensions.ContainerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Extensions.ContainerClasses
{
    [TestClass]
    public class CollectionExtensionTests
    {
        [TestMethod]
        public void TestCollectionAddRange()
        {
            var coll = new Collection<int> {1, 2};
            coll.AddRange(new List<int>{3});
        }
    }
}