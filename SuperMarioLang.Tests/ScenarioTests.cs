using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace SuperMarioLang.Tests
{
    [TestClass]
    public class ScenarioTests
    {
        private Scenario scenarioUT;
        private Mock<ICellFactory> factoryMock;

        [TestInitialize]
        public void SetUp()
        {
            factoryMock = new Mock<ICellFactory>();
            factoryMock.Setup(fm => fm.Create(It.IsAny<int>(), It.IsAny<int>(), ' ')).Returns<int, int, char>((i, j, _) => new Cell(i, j, CellType.USELESS));
            factoryMock.Setup(fm => fm.Create(It.IsAny<int>(), It.IsAny<int>(), '=')).Returns<int, int, char>((i, j, _) => new Cell(i, j, CellType.FLOOR));
            factoryMock.Setup(fm => fm.Create(It.IsAny<int>(), It.IsAny<int>(), '"')).Returns<int, int, char>((i, j, _) => new Cell(i, j, CellType.ELEVATOR_END));
            factoryMock.Setup(fm => fm.Create(It.IsAny<int>(), It.IsAny<int>(), '#')).Returns<int, int, char>((i, j, _) => new Cell(i, j, CellType.ELEVATOR_START));
        }

        [TestMethod]
        public void ShouldCreateABasicScenario()
        {
            scenarioUT = new Scenario(new string[] { "++,", "====" }, factoryMock.Object);
            factoryMock.Verify(fm => fm.Create(It.IsAny<int>(), It.IsAny<int>(), '+'), Times.Exactly(2));
            factoryMock.Verify(fm => fm.Create(It.IsAny<int>(), It.IsAny<int>(), ','), Times.Once());
            factoryMock.Verify(fm => fm.Create(It.IsAny<int>(), It.IsAny<int>(), '='), Times.Exactly(4));
        }

        [TestMethod]
        public void StartsAtTheProperPosition()
        {
            scenarioUT = new Scenario(new string[] { " ", "=" }, factoryMock.Object);

            Assert.AreEqual(0, scenarioUT.InitialPosition.X);
            Assert.AreEqual(0, scenarioUT.InitialPosition.Y);
            Assert.AreEqual(CellType.USELESS, scenarioUT.InitialPosition.Type);
        }

        [TestMethod]
        public void CalculatesTheNextPositionWhenGoingRight()
        {
            scenarioUT = new Scenario(new string[] { "  ", "==" }, factoryMock.Object);
            var actual = scenarioUT.NextPosition(
                new Mario() { X = 0, Y = 0, Direction = Movement.RIGHT });

            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(1, actual.Y);
        }

        [TestMethod]
        public void CalculatesTheNextPositionWhenGoingLeft()
        {
            scenarioUT = new Scenario(new string[] { "  ", "==" }, factoryMock.Object);
            var actual = scenarioUT.NextPosition(
                new Mario() { X = 0, Y = 1, Direction = Movement.LEFT });

            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(0, actual.Y);
        }

        [TestMethod]
        public void CalculatesTheNextPositionWhenFalling()
        {
            scenarioUT = new Scenario(new string[] { "  ", " =" }, factoryMock.Object);
            var actual = scenarioUT.NextPosition(
                new Mario() { X = 0, Y = 0, Direction = Movement.RIGHT });

            Assert.AreEqual(1, actual.X);
            Assert.AreEqual(0, actual.Y);
        }

        [TestMethod]
        public void CalculatesTheNextPositionWhenStoppedOverAnElevatorUp()
        {
            scenarioUT = new Scenario(new string[] { "\"", " ", "#" }, factoryMock.Object);
            var actual = scenarioUT.NextPosition(
                new Mario() { X = 1, Y = 0, Direction = Movement.STOP });

            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(0, actual.Y);
        }

        [TestMethod]
        public void CalculatesTheNextPositionWhenStoppedOverAnElevatorDown()
        {
            scenarioUT = new Scenario(new string[] { " ", "#", " ", "\"" }, factoryMock.Object);
            var actual = scenarioUT.NextPosition(
                new Mario() { X = 0, Y = 0, Direction = Movement.STOP });

            Assert.AreEqual(1, actual.X);
            Assert.AreEqual(0, actual.Y);
        }

        [TestMethod]
        public void CalculatesTheNextPositionWhenGoingUp()
        {
            scenarioUT = new Scenario(new string[] { "\"", " ", " ", "#" }, factoryMock.Object);
            var actual = scenarioUT.NextPosition(
                new Mario() { X = 1, Y = 0, Direction = Movement.ELEVATOR_UP });

            Assert.AreEqual(0, actual.X);
            Assert.AreEqual(0, actual.Y);
        }

        [TestMethod]
        public void CalculatesTheNextPositionWhenGoingDown()
        {
            scenarioUT = new Scenario(new string[] { " ", "#", " ", "\"" }, factoryMock.Object);
            var actual = scenarioUT.NextPosition(
                new Mario() { X = 1, Y = 0, Direction = Movement.ELEVATOR_DOWN });

            Assert.AreEqual(2, actual.X);
            Assert.AreEqual(0, actual.Y);
        }

        [TestMethod]
        public void CalculatesABlockingPosition()
        {
            scenarioUT = new Scenario(new string[] { " =", "==" }, factoryMock.Object);

            Assert.ThrowsException<Exception>(() =>
            {
                var actual = scenarioUT.NextPosition(
                    new Mario() { X = 0, Y = 0, Direction = Movement.RIGHT });
            });
        }

        [TestMethod]
        public void CalculatesAnEndgamePosition()
        {
            scenarioUT = new Scenario(new string[] { "  ", "==" }, factoryMock.Object);
            var actual = scenarioUT.NextPosition(
                new Mario() { X = 0, Y = 1, Direction = Movement.RIGHT });

            Assert.AreEqual(-1, actual.X);
            Assert.AreEqual(-1, actual.Y);
            Assert.AreEqual(CellType.END, actual.Type);
        }

        [TestMethod]
        public void ThrowsAnErrorWhenAnElevatorDoesNotHaveACorrespondingEnd()
        {
            scenarioUT = new Scenario(new string[] { " ", " ", "#" }, factoryMock.Object);

            try
            {
                scenarioUT.NextPosition(
                    new Mario() { X = 1, Y = 0, Direction = Movement.STOP });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "Elevator");
            }
        }
    }
}
