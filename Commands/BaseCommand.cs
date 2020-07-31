using McMaster.Extensions.CommandLineUtils;
using System;
using System.Threading.Tasks;

namespace CrazyScraper.Commands
{
    [HelpOption("-h|--help")]
    public class BaseCommand
    {
        public BaseCommand()
        {

        }
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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }
        protected void OutputWarning(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Out.WriteLine(message);
            Console.ResetColor();
        }
    }
}