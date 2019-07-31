using System;
using System.Collections.Generic;

namespace SuperMarioLang
{
    public class ArgsReader : IArgsReader
    {
        private string arguments;

        public void SetArguments(IEnumerable<string> args)
        {
            arguments = string.Join(' ', args);
        }

        public int GetNumber()
        {
            var splitted = arguments.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (splitted?.Length == 0) return 0;

            var res = int.Parse(splitted[0]);
            arguments = arguments.Substring(arguments.IndexOf("" + res) + ("" + res).Length);
            if (arguments.Length > 0 && arguments[0] == ' ') arguments = arguments.Substring(1);
            return res;
        }

        public char GetChar()
        {
            if (arguments.Length == 0) return (char)0;

            var res = arguments[0];
            arguments = arguments.Substring(1);
            return res;
        }
    }
}
