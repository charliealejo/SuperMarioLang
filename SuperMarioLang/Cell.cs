using System;

namespace SuperMarioLang
{
    internal class Cell
    {
        internal static Cell Endgame = new Cell(-1, -1, CellType.END);

        internal CellType Type { get; private set; }

        internal int X { get; private set; }

        internal int Y { get; private set; }

        internal Cell(int i, int j, char c) : this(i, j, Translate(c)) { }

        private Cell(int i, int j, CellType t)
        {
            X = i;
            Y = j;
            Type = t;
        }

        internal bool IsFloor()
        {
            return Type == CellType.FLOOR ||
                   Type == CellType.WALL ||
                   Type == CellType.ELEVATOR_END ||
                   Type == CellType.ELEVATOR_START;
        }

        internal bool IsInstruction()
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
                   Type == CellType.WRITE_NUMBER;
        }

        internal bool IsMovement()
        {
            return Type == CellType.GO_LEFT ||
                   Type == CellType.GO_RIGHT ||
                   Type == CellType.JUMP ||
                   Type == CellType.STOP ||
                   Type == CellType.TURN_AROUND;
        }

        private static CellType Translate(char c)
        {
            switch (c)
            {
                case '=': return CellType.FLOOR;
                case '|': return CellType.WALL;
                case '#': return CellType.ELEVATOR_START;
                case '"': return CellType.ELEVATOR_END;

                case ')': return CellType.TAPE_RIGHT;
                case '(': return CellType.TAPE_LEFT;
                case '+': return CellType.TAPE_INCR;
                case '-': return CellType.TAPE_DECR;
                case '%': return CellType.TAPE_JUMP;
                case '\'': return CellType.TAPE_INDEX;
                case '&': return CellType.TAPE_RETRIEVE;
                case '.': return CellType.WRITE_CHAR;
                case ':': return CellType.WRITE_NUMBER;
                case ',': return CellType.READ_CHAR;
                case ';': return CellType.READ_NUMBER;

                case '>': return CellType.GO_RIGHT;
                case '<': return CellType.GO_LEFT;
                case '^': return CellType.JUMP;
                case '!': return CellType.STOP;
                case '[': return CellType.BRANCH;
                case '@': return CellType.TURN_AROUND;

                default: return CellType.USELESS;
            }
        }
    }

    internal enum CellType
    {
        FLOOR,
        WALL,
        ELEVATOR_START,
        ELEVATOR_END,
        TAPE_RIGHT,
        TAPE_LEFT,
        TAPE_JUMP,
        TAPE_INDEX,
        TAPE_RETRIEVE,
        TAPE_INCR,
        TAPE_DECR,
        STOP,
        JUMP,
        GO_LEFT,
        GO_RIGHT,
        TURN_AROUND,
        BRANCH,
        READ_NUMBER,
        READ_CHAR,
        WRITE_NUMBER,
        WRITE_CHAR,
        USELESS,
        END
    }
}