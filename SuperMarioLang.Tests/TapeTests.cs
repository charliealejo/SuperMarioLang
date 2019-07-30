using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperMarioLang.Tests
{
    [TestClass]
    public class TapeTests
    {
        private Tape tapeUT;

        [TestInitialize]
        public void SetUp()
        {
            tapeUT = new Tape(256);
            tapeUT.Start();
        }

        [TestMethod]
        public void MovingAroundTheTape()
        {
            Assert.AreEqual(0, tapeUT.GetValue());
            tapeUT.Increment();
            Assert.AreEqual(1, tapeUT.GetValue());
            tapeUT.MoveRight();
            Assert.AreEqual(0, tapeUT.GetValue());
            tapeUT.MoveLeft();
            Assert.AreEqual(1, tapeUT.GetValue());
            tapeUT.MoveLeft();
            Assert.AreEqual(0, tapeUT.GetValue());
            tapeUT.MoveRight();
            Assert.AreEqual(1, tapeUT.GetValue());
        }

        [TestMethod]
        public void JumpingAroundTheTape()
        {
            tapeUT.SetValue(10);
            tapeUT.Jump();
            Assert.AreEqual(0, tapeUT.GetValue());
            tapeUT.SetIndex();
            Assert.AreEqual(10, tapeUT.GetValue());
            tapeUT.Decrement();
            Assert.AreEqual(9, tapeUT.GetValue());
            tapeUT.Retrieve();
            Assert.AreEqual(0, tapeUT.GetValue());
        }
    }
}
