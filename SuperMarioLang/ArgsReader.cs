using System;
using System.Collections.Generic;

namespace SuperMarioLang
{
    internal class ArgsReader
    {
        private string arguments;

        internal ArgsReader(IEnumerable<string> args)
        {
            arguments = string.Join(' ', args);
        }

        internal int GetNumber()
        {
            var splitted = arguments.Split(' ');
            if (splitted?.Length == 0) throw new Exception("No arguments to read");
            var res = int.Parse(splitted[0]);
            arguments = arguments.Substring(arguments.IndexOf("" + res) + ("" + res).Length);
            if (arguments.Length > 0 && arguments[0] == ' ') arguments = arguments.Substring(1);
            return res;
        }

        internal char GetChar()
        {
            if (arguments.Length == 0) throw new Exception("No arguments to read");
            var res = arguments[0];
            arguments = arguments.Substring(1);
            return res;
        }
    }
}
