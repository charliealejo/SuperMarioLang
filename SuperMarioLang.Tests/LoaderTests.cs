using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SuperMarioLang.Tests
{
    [TestClass]
    public class LoaderTests
    {
        private Loader loaderUT;

        [TestInitialize]
        public void SetUp()
        {
            loaderUT = new Loader();
        }

        [TestMethod]
        public void ReadsAFile_Part1()
        {
            var actual = loaderUT.Load(@"..\..\..\..\Examples\HelloWorld.sml");
            Assert.AreEqual(17, actual.Count());
        }

        [TestMethod]
        public void ReadsAFile_Part2()
        {
            var actual = loaderUT.Load(@"..\..\..\..\Examples\CharFreq.sml");
            Assert.AreEqual(12, actual.Count());
        }

        [TestMethod]
        public void WrongPath()
        {
            var actual = loaderUT.Load(@"..\..\..\..\Examples\DoesNotExist.sml");
            Assert.IsNull(actual);
        }
    }
}
