using System;
using System.IO;

namespace SuperMarioLang
{
    internal class Loader
    {
        public Loader()
        {
        }

        internal Scenario Load(string path)
        {
            if (new FileInfo(path).Length == 0) return null;

            string[] scenario = File.ReadAllLines(path);
            return new Scenario(scenario);
        }
    }
}