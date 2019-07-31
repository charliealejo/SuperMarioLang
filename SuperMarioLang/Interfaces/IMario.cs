namespace SuperMarioLang
{
    public interface IMario
    {
        Movement Direction { get; set; }
        int X { get; set; }
        int Y { get; set; }

        void Start();
    }
}