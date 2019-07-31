using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using LoaderUT = SuperMarioLang.Loader;

namespace SuperMarioLang.Tests
{
    [TestClass]
    public class LoaderTests
    {
        [TestMethod]
        public void ReadsAFile_Part1()
        {
            var actual = LoaderUT.Load(@"..\..\..\..\Examples\HelloWorld.sml");
            Assert.AreEqual(17, actual.Count());
        }

        [TestMethod]
        public void ReadsAFile_Part2()
        {
            var actual = LoaderUT.Load(@"..\..\..\..\Examples\CharFreq.sml");
            Assert.AreEqual(12, actual.Count());
        }

        [TestMethod]
        public void WrongPath()
        {
            var actual = LoaderUT.Load(@"..\..\..\..\Examples\DoesNotExist.sml");
            Assert.IsNull(actual);
        }
    }
}
