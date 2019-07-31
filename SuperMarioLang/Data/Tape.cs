using System;

namespace SuperMarioLang
{
    public class Tape : ITape
    {
        private int[] tape;

        private int position;

        private readonly int capacity;

        public Tape(int capacity)
        {
            this.capacity = capacity;
        }

        public void Start()
        {
            tape = new int[capacity];
            position = 0;
        }

        public void MoveRight() => position = ++position == capacity ? 0 : position;

        public void MoveLeft() => position = --position < 0 ? capacity - 1 : position;

        public void Increment() => tape[position]++;

        public void Decrement() => tape[position]--;

        public int GetValue() => tape[position];

        public void SetValue(char v) => tape[position] = (byte)v;

        public void SetValue(int v) => tape[position] = v;

        public void Jump() => position = GetValue();

        public void SetIndex() => SetValue(position);

        public void Retrieve() => SetValue(tape[GetValue()]);
    }
}