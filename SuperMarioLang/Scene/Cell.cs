using System;

namespace SuperMarioLang
{
    public class Cell
    {
        public static Cell Endgame = new Cell(-1, -1, CellType.END);

        public CellType Type { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public Cell(int i, int j, CellType t)
        {
            X = i;
            Y = j;
            Type = t;
        }

        public override string ToString()
        {
            return $"Cell at ({X},{Y}), type {Type}";
        }

        public bool IsFloor()
        {
            return Type == CellType.FLOOR ||
                   Type == CellType.WALL ||
                   Type == CellType.ELEVATOR_END ||
                   Type == CellType.ELEVATOR_START;
        }

        public bool IsInstruction()
        {
            return Type == CellType.BRANCH ||
                   Type == CellType.READ_CHAR ||
                   Type == CellType.READ_NUMBER ||
                   Type == CellType.TAPE_DECR ||
                   Type == CellType.TAPE_INCR ||
                   Type == CellType.TAPE_LEFT ||
                   Type == CellType.TAPE_RIGHT ||
                   Type == CellType.TAPE_JUMP ||
                   Type == CellType.TAPE_INDEX ||
                   Type == CellType.TAPE_RETRIEVE ||
                   Type == CellType.WRITE_CHAR ||
                   Type == CellType.WRITE_NUMBER
#if DEBUG
                   || Type == CellType.DEBUG
#endif
                   ;
        }

        public bool IsMovement()
        {
            return Type == CellType.GO_LEFT ||
                   Type == CellType.GO_RIGHT ||
                   Type == CellType.JUMP ||
                   Type == CellType.STOP ||
                   Type == CellType.TURN_AROUND;
        }
    }
}