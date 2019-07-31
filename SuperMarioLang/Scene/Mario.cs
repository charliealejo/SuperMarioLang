using System;

namespace SuperMarioLang
{
    public class Mario : IMario
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Movement Direction { get; set; }

        public void Start()
        {
            X = 0;
            Y = 0;
            Direction = Movement.RIGHT;
        }
    }

    public enum Movement
    {
        LEFT,
        RIGHT,
        STOP,
        JUMP,
        ELEVATOR_UP,
        ELEVATOR_DOWN
    }
}