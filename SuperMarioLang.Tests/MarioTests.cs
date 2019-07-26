using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperMarioLang.Tests
{
    [TestClass]
    public class MarioTests
    {
        private Mario marioUT;

        [TestInitialize]
        public void SetUp()
        {
            marioUT = new Mario();
        }

        [TestMethod]
        public void ShouldInitializeMarioProperly()
        {
            Assert.AreEqual(0, marioUT.X);
            Assert.AreEqual(0, marioUT.Y);

            marioUT.Start();

            Assert.AreEqual(Movement.RIGHT, marioUT.Direction);
        }
    }
}
