namespace SuperMarioLang
{
    internal class CellFactory : ICellFactory
    {
        public Cell Create(int x, int y, char c)
        {
            return new Cell(x, y, Translate(c));
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
                case '&': return CellType.TAPE_INDEX;
                case '*': return CellType.TAPE_RETRIEVE;
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

                case '?': return CellType.DEBUG;

                default: return CellType.USELESS;
            }
        }
    }
}
