using System.Collections.Generic;

namespace SuperMarioLang
{
    public interface IArgsReader
    {
        char GetChar();
        int GetNumber();
        void SetArguments(IEnumerable<string> args);
    }
}