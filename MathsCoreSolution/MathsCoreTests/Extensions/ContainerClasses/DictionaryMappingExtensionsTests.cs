using System.Collections.Generic;
using CSharpExtensionsTests;
using CSharpExtensionsTests.GeneralFixtures;
using MathsCore.Extensions.ContainerClasses;
using MathsCore.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathsCoreTests.Extensions.ContainerClasses
{
    /// <summary>
    /// Summary description for DictionaryMappingExtensionsTests
    /// </summary>
    [TestClass]
    public class DictionaryMappingExtensionsTests
    {
        #region restriction, image and preimage

        [TestMethod]
        public void TestRestrict()
        {
            DictionaryFixtures.Dict.Restrict(DictionaryFixtures.Dict.Keys.ToSet()).ShouldBeEquivalentMappingTo(DictionaryFixtures.Dict);
            DictionaryFixtures.Dict.Restrict(new Set<string> { "a" }).ShouldBeEquivalentMappingTo(new Dictionary<string, int> { { "a", 1 } });
            DictionaryFixtures.Dict.Restrict(new Set<string>()).ShouldBeEquivalentMappingTo(new Dictionary<string, int>());
        }

        [TestMethod]
        public void TestImage()
        {
            DictionaryFixtures.Dict.Image(DictionaryFixtures.Dict.Keys.ToSet()).ShouldEqual(DictionaryFixtures.Dict.Values.ToSet());
            DictionaryFixtures.Dict.Image(new Set<string> { "a" }).ShouldEqual(new Set<int> { 1 });
            DictionaryFixtures.Dict.Image(new Set<string>()).ShouldEqual(new Set<int>());
        }

        [TestMethod]
        public void TestPreimage()
        {
            var dict = new Dictionary<string, int>
                {
                    {"a", 2},
                    {"b", 3},
                    {"c", 2}
                };
            dict.Preimage(dict.Values.ToSet()).ShouldEqual(dict.Keys.ToSet());
            dict.Preimage(new Set<int> { 2 }).ShouldEqual(new Set<string> { "a", "c" });
            dict.Preimage(new Set<int>()).ShouldEqual(new Set<string>());
        }

        [TestMethod]
        public void TestRestrictValues()
        {
            DictionaryFixtures.Dict.Restrict(DictionaryFixtures.Dict.Values.ToSet()).ShouldBeEquivalentMappingTo(DictionaryFixtures.Dict);
            DictionaryFixtures.Dict.Restrict(new Set<int>()).ShouldBeEquivalentMappingTo(new Dictionary<string, int>());
            DictionaryFixtures.Dict.Restrict(new Set<int> { 7 }).ShouldBeEquivalentMappingTo(new Dictionary<string, int> { { "b", 7 } });
        }

        #endregion

        #region filtering

        [TestMethod]
        public void WhereKeysTest()
        {
            DictionaryFixtures.Dict.WhereKeys(key => key == "a" || key == "c")
                .ShouldBeEquivalentMappingTo(new Dictionary<string, int>
                    {
                        {"a", 1}, {"c", 9}
                    });
        }

        [TestMethod]
        public void WhereValuesTest()
        {
            DictionaryFixtures.Dict.WhereValues(value => value > 1)
                .ShouldBeEquivalentMappingTo(new Dictionary<string, int>
                    {
                        {"b", 7}, {"c", 9}
                    });
        }

        #endregion
    }
}
