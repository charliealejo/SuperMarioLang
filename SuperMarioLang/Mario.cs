using System;

namespace SuperMarioLang
{
    internal class Mario
    {
        internal int X { get; set; }

        internal int Y { get; set; }

        internal Movement Direction { get; set; }

        internal void Start()
        {
            X = 0;
            Y = 0;
            Direction = Movement.RIGHT;
        }
    }

    internal enum Movement
    {
        LEFT,
        RIGHT,
        STOP,
        JUMP,
        ELEVATOR_UP,
        ELEVATOR_DOWN
    }
}