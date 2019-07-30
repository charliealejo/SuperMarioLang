using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperMarioLang.Tests
{
    [TestClass]

    public class ArgsReaderTests
    {
        private ArgsReader readerUT;

        [TestInitialize]
        public void SetUp()
        {
            readerUT = new ArgsReader();
        }

        [TestMethod]
        public void ReadsACharacter()
        {
            readerUT.SetArguments(new string[] { "c" });

            var actual = readerUT.GetChar();
            var expected = 'c';

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadsANumber()
        {
            readerUT.SetArguments(new string[] { " 42 " });

            var actual = readerUT.GetNumber();
            var expected = 42;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadsACharacterBeforeANumber()
        {
            readerUT.SetArguments(new string[] { "c 42" });

            var actual = readerUT.GetChar();
            var expected = 'c';

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadsANumberBeforeAChar()
        {
            readerUT.SetArguments(new string[] { "42 c" });

            var actual = readerUT.GetNumber();
            var expected = 42;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadZeroCharWhenStringIsEmpty()
        {
            readerUT.SetArguments(new string[] { "" });

            var actual = readerUT.GetChar();
            var expected = 0;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadZeroNumberWhenStringIsEmpty()
        {
            readerUT.SetArguments(new string[] { "" });

            var actual = readerUT.GetNumber();
            var expected = 0;

            Assert.AreEqual(expected, actual);
        }
    }
}
