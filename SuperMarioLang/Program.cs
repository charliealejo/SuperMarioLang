using System;
using System.IO;
using System.Linq;

namespace SuperMarioLang
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * TODO: Add parameters to help debugging the code. (Flag: -d)
             * TODO: Add parameters to modify the size of the stack. (Flag: -s 1024)
             */
            try
            {
                if (CheckParameters(args))
                {
                    var interpreter = new Interpreter(new Loader());
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
            Console.WriteLine("    SuperMarioLang <FileToOpen> [<args>]");
            Console.WriteLine();
            Console.WriteLine("Parameters:");
            Console.WriteLine("    <FileToOpen>  The path to the file with the code to execute");
            Console.WriteLine("    <args>        The optional arguments to pass to the code");
        }
    }
}
