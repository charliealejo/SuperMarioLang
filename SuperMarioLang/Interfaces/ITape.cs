namespace SuperMarioLang
{
    public interface ITape
    {
        void Decrement();
        int GetValue();
        void Increment();
        void Jump();
        void MoveLeft();
        void MoveRight();
        void Retrieve();
        void SetIndex();
        void SetValue(char v);
        void SetValue(int v);
        void Start();
    }
}