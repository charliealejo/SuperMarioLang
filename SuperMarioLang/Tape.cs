using System;

namespace SuperMarioLang
{
    internal class Tape
    {
        internal int[] tape;

        internal int position;

        internal int capacity;

        internal Tape(int capacity)
        {
            this.capacity = capacity;
        }

        internal void Start()
        {
            tape = new int[capacity];
            position = 0;
        }

        internal void MoveRight() => position = ++position == capacity ? 0 : position;

        internal void MoveLeft() => position = --position < 0 ? capacity - 1 : position;

        internal void Increment() => tape[position]++;

        internal void Decrement() => tape[position]--;

        internal int GetValue() => tape[position];

        internal void SetValue(char v) => tape[position] = (byte)v;

        internal void SetValue(int v) => tape[position] = v;

        internal void Jump() => position = GetValue();

        internal void SetIndex() => SetValue(position);

        internal void Retrieve() => SetValue(tape[GetValue()]);
    }
}