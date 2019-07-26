using System.IO;

namespace SuperMarioLang
{
    internal class Loader
    {
        public Loader()
        {
        }

        internal string[] Load(string path)
        {
            if (new FileInfo(path).Length == 0) return null;

            return File.ReadAllLines(path);
        }
    }
}