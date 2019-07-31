namespace SuperMarioLang
{
    public interface IScenario
    {
        Cell InitialPosition { get; }

        Cell NextPosition(IMario mario);
    }
}