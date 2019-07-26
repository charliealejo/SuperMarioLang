namespace SuperMarioLang
{
    public interface ICellFactory
    {
        Cell Create(int x, int y, char c);
    }
}