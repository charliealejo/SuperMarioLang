using System;
using System.IO;

namespace SuperMarioLang
{
    public class Loader
    {
        public Loader()
        {
        }

        public string[] Load(string path)
        {
            try
            {
                return File.ReadAllLines(path);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}