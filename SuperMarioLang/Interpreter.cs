using System;
using System.Collections.Generic;

namespace SuperMarioLang
{
    internal class Interpreter
    {
        private Loader loader;
        private ArgsReader reader;
        private Tape tape;
        private bool skip;
        private Mario mario;

#if DEBUG
        private readonly List<Cell> route = new List<Cell>();
#endif

        public Interpreter(Loader loader)
        {
            this.loader = loader;
            tape = new Tape(256);
            skip = false;
        }

        internal void Execute(string path, IEnumerable<string> args)
        {
            var scenario = loader.Load(path);
            if (scenario == null) return;

            reader = new ArgsReader(args);
            mario = new Mario();
            var currentCell = scenario.InitialPosition;

            while (currentCell.Type != CellType.END)
            {
#if DEBUG
                route.Add(currentCell);
#endif
                if (currentCell.IsInstruction())
                {
                    if (skip) skip = false;
                    else ExecuteInstruction(currentCell.Type);
                }
                else if (currentCell.IsMovement())
                {
                    if (skip) skip = false;
                    else ChangeMovement(currentCell.Type);
                }
                currentCell = scenario.NextPosition(mario);
                mario.X = currentCell.X;
                mario.Y = currentCell.Y;
            }
        }

        private void ExecuteInstruction(CellType type)
        {
            switch (type)
            {
                case CellType.TAPE_RIGHT:
                    tape.MoveRight();
                    break;
                case CellType.TAPE_LEFT:
                    tape.MoveLeft();
                    break;
                case CellType.TAPE_INCR:
                    tape.Increment();
                    break;
                case CellType.TAPE_DECR:
                    tape.Decrement();
                    break;
                case CellType.TAPE_JUMP:
                    tape.Jump();
                    break;
                case CellType.TAPE_INDEX:
                    tape.SetIndex();
                    break;
                case CellType.TAPE_RETRIEVE:
                    tape.Retrieve();
                    break;
                case CellType.BRANCH:
                    if (tape.GetValue() == 0) skip = true;
                    break;
                case CellType.READ_NUMBER:
                    tape.SetValue(reader.GetNumber());
                    break;
                case CellType.READ_CHAR:
                    tape.SetValue(reader.GetChar());
                    break;
                case CellType.WRITE_NUMBER:
                    Console.Write(tape.GetValue() + " ");
                    break;
                case CellType.WRITE_CHAR:
                    Console.Write((char)tape.GetValue());
                    break;
            }
        }

        private void ChangeMovement(CellType type)
        {
            switch (type)
            {
                case CellType.STOP:
                    mario.Direction = Movement.STOP;
                    break;
                case CellType.JUMP:
                    mario.Direction = Movement.JUMP;
                    break;
                case CellType.GO_LEFT:
                    mario.Direction = Movement.LEFT;
                    break;
                case CellType.GO_RIGHT:
                    mario.Direction = Movement.RIGHT;
                    break;
                case CellType.TURN_AROUND:
                    mario.Direction = mario.Direction == Movement.RIGHT ? Movement.LEFT :
                        mario.Direction == Movement.LEFT ? Movement.RIGHT : mario.Direction;
                    break;
            }
        }
    }
}