using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SuperMarioLang.Tests
{
    [TestClass]
    public class InterpreterTests
    {
        private Interpreter interpreterUT;

        private Mock<IArgsReader> argsReaderMock;
        private Mock<ITape> tapeMock;
        private Mock<IMario> marioMock;
        private Mock<IScenario> scenarioMock;

        [TestInitialize]
        public void SetUp()
        {
            argsReaderMock = new Mock<IArgsReader>();
            tapeMock = new Mock<ITape>();
            marioMock = new Mock<IMario>();
            scenarioMock = new Mock<IScenario>();

            tapeMock.Setup(tm => tm.GetValue()).Returns(42);
            argsReaderMock.Setup(arm => arm.GetChar()).Returns('A');
            argsReaderMock.Setup(arm => arm.GetNumber()).Returns(113);

            interpreterUT = new Interpreter(
                argsReaderMock.Object, tapeMock.Object, marioMock.Object);
        }

        [TestMethod]
        public void ParsesCommands()
        {
            scenarioMock.SetupGet(sm => sm.InitialPosition).Returns(
                new Cell(0, 0, CellType.USELESS));
            scenarioMock.SetupSequence(sm => sm.NextPosition(It.IsAny<IMario>()))
                .Returns(new Cell(0, 1, CellType.READ_CHAR))
                .Returns(new Cell(0, 2, CellType.WRITE_CHAR))
                .Returns(new Cell(0, 3, CellType.READ_NUMBER))
                .Returns(new Cell(0, 4, CellType.WRITE_NUMBER))
                .Returns(new Cell(0, 5, CellType.TAPE_DECR))
                .Returns(new Cell(0, 6, CellType.TAPE_INCR))
                .Returns(new Cell(0, 7, CellType.TAPE_INDEX))
                .Returns(new Cell(0, 8, CellType.TAPE_JUMP))
                .Returns(new Cell(0, 9, CellType.TAPE_RETRIEVE))
                .Returns(new Cell(0, 10, CellType.TAPE_LEFT))
                .Returns(new Cell(0, 11, CellType.TAPE_RIGHT))
                .Returns(new Cell(-1, -1, CellType.END));

            interpreterUT.Execute(scenarioMock.Object, new string[] { "" });

            tapeMock.Verify(tm => tm.Start(), Times.Once());
            tapeMock.Verify(tm => tm.Decrement(), Times.Once());
            tapeMock.Verify(tm => tm.Increment(), Times.Once());
            tapeMock.Verify(tm => tm.GetValue(), Times.Exactly(2));
            tapeMock.Verify(tm => tm.Jump(), Times.Once());
            tapeMock.Verify(tm => tm.MoveLeft(), Times.Once());
            tapeMock.Verify(tm => tm.MoveRight(), Times.Once());
            tapeMock.Verify(tm => tm.Retrieve(), Times.Once());
            tapeMock.Verify(tm => tm.SetIndex(), Times.Once());
            tapeMock.Verify(tm => tm.SetValue('A'), Times.Once());
            tapeMock.Verify(tm => tm.SetValue(113), Times.Once());

            marioMock.Verify(mm => mm.Start(), Times.Once());
            marioMock.VerifySet(mm => mm.X = It.IsAny<int>(), Times.Exactly(12));
            marioMock.VerifySet(mm => mm.Y = It.IsAny<int>(), Times.Exactly(12));
            marioMock.VerifySet(mm => mm.Direction = It.IsAny<Movement>(), Times.Never());
        }

        [TestMethod]
        public void ParsesMarioMovements()
        {
            scenarioMock.SetupGet(sm => sm.InitialPosition).Returns(
                new Cell(0, 0, CellType.USELESS));
            scenarioMock.SetupSequence(sm => sm.NextPosition(It.IsAny<IMario>()))
                .Returns(new Cell(0, 1, CellType.BRANCH))
                .Returns(new Cell(0, 2, CellType.GO_LEFT))
                .Returns(new Cell(0, 3, CellType.GO_RIGHT))
                .Returns(new Cell(0, 4, CellType.STOP))
                .Returns(new Cell(0, 5, CellType.TURN_AROUND))
                .Returns(new Cell(0, 6, CellType.JUMP))
                .Returns(new Cell(-1, -1, CellType.END));

            interpreterUT.Execute(scenarioMock.Object, new string[] { "" });

            tapeMock.Verify(tm => tm.Start(), Times.Once());
            tapeMock.Verify(tm => tm.Decrement(), Times.Never());
            tapeMock.Verify(tm => tm.Increment(), Times.Never());
            tapeMock.Verify(tm => tm.GetValue(), Times.Once()); // BRANCH
            tapeMock.Verify(tm => tm.Jump(), Times.Never());
            tapeMock.Verify(tm => tm.MoveLeft(), Times.Never());
            tapeMock.Verify(tm => tm.MoveRight(), Times.Never());
            tapeMock.Verify(tm => tm.Retrieve(), Times.Never());
            tapeMock.Verify(tm => tm.SetIndex(), Times.Never());
            tapeMock.Verify(tm => tm.SetValue(It.IsAny<char>()), Times.Never());
            tapeMock.Verify(tm => tm.SetValue(It.IsAny<int>()), Times.Never());

            marioMock.Verify(mm => mm.Start(), Times.Once());
            marioMock.VerifySet(mm => mm.X = It.IsAny<int>(), Times.Exactly(7));
            marioMock.VerifySet(mm => mm.Y = It.IsAny<int>(), Times.Exactly(7));
            marioMock.VerifySet(mm => mm.Direction = Movement.RIGHT, Times.Exactly(2));
            marioMock.VerifySet(mm => mm.Direction = Movement.LEFT, Times.Once());
            marioMock.VerifySet(mm => mm.Direction = Movement.STOP, Times.Once());
            marioMock.VerifySet(mm => mm.Direction = Movement.JUMP, Times.Once());
        }
    }
}
