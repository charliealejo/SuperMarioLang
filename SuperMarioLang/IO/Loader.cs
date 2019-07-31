using System;
using System.IO;

namespace SuperMarioLang
{
    public static class Loader
    {
        public static string[] Load(string path)
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