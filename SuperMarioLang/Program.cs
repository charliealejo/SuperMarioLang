using System;
using System.IO;
using System.Linq;

namespace SuperMarioLang
{
    class Program
    {
        static void Main(string[] args)
        {
            if (CheckParameters(args))
            {
                var interpreter = new Interpreter(new Loader());
                try
                {
                    interpreter.Execute(args[0], args.Skip(1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"SuperMarioException: {ex.Message}");
                }
            }
            else DisplayUsage();

#if DEBUG
            Console.ReadKey();
#endif
        }

        private static bool CheckParameters(string[] args)
        {
            return args.Length >= 1 && File.Exists(args[0]);
        }

        private static void DisplayUsage()
        {
            Console.WriteLine("SuperMarioLang interpreter.");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine();
            Console.WriteLine("    SuperMarioLang <FileToOpen> [<args>]");
            Console.WriteLine();
            Console.WriteLine("Parameters:");
            Console.WriteLine("    <FileToOpen>  The path to the file with the code to execute");
            Console.WriteLine("    <args>        The optional arguments to pass to the code");
        }
    }
}
