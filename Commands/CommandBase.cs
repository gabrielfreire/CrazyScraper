using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrazyScraper.Commands
{
    [HelpOption("--help")]
    public class CommandBase
    {
        protected virtual Task<int> OnExecute(CommandLineApplication app)
        {
            return Task.FromResult(0);
        }

        protected void OutputToConsole(string data)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.Write(data);
            Console.ResetColor();
        }

        protected void OutputError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Error.WriteLine($"ERROR {message}");
            Console.ResetColor();
        }
        protected void OutputWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Out.WriteLine($"WARNING {message}");
            Console.ResetColor();
        }
    }
}
