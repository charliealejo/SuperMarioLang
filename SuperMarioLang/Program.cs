using System;
using System.IO;
using System.Linq;

namespace SuperMarioLang
{
    class Program
    {
        static int size = 256;
        static bool debug = false;

        static void Main(string[] args)
        {
            try
            {
                SetFlags(ref args);
                if (CheckParameters(args))
                {
                    var interpreter = new Interpreter(
                        new Loader(), new ArgsReader(), new Tape(size), new Mario(), debug);
                    interpreter.Execute(args[0], args.Skip(1));
                }
                else DisplayUsage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SuperMarioException: {ex.Message}");
            }

#if DEBUG
            Console.ReadKey();
#endif
        }

        private static void SetFlags(ref string[] args)
        {
            while (args.First().StartsWith('-'))
            {
                var arg = args.First();
                args = args.Skip(1).ToArray();
                switch (arg[1])
                {
                    case 'd': debug = true; break;
                    case 's':
                        var s = args.First();
                        args = args.Skip(1).ToArray();
                        size = int.Parse(s);
                        break;
                }
            }
        }

        private static bool CheckParameters(string[] args)
        {
            if (args.Length > 0)
            {
                if (File.Exists(args[0])) return true;
                else throw new Exception("File does not exist.");
            }
            return false;
        }

        private static void DisplayUsage()
        {
            Console.WriteLine("SuperMarioLang interpreter.");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine();
            Console.WriteLine("    SuperMarioLang [-d] [-s N] <FileToOpen> [<args>]");
            Console.WriteLine();
            Console.WriteLine("Parameters:");
            Console.WriteLine("    -d            Debug mode, writes information about what Mario did");
            Console.WriteLine("    -s N          Sets the tape size to N bytes");
            Console.WriteLine("    <FileToOpen>  The path to the file with the code to execute");
            Console.WriteLine("    <args>        The optional arguments to pass to the code");
        }
    }
}
