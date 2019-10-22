using McMaster.Extensions.CommandLineUtils;
using System;
using System.Threading.Tasks;

namespace CrazyScraper
{
    [HelpOption("--help")]
    public class CrazyScraperCmdBase
    {
        protected IConsole _console;
        public CrazyScraperCmdBase()
        {

        }
        protected virtual Task<int> OnExecute(CommandLineApplication app)
        {
            return Task.FromResult(0);
        }

        protected void OutputToConsole(string data)
        {
            _console.BackgroundColor = ConsoleColor.Black;
            _console.ForegroundColor = ConsoleColor.White;
            _console.Out.Write(data);
            _console.ResetColor();
        }

        protected void OutputError(string message)
        {
            _console.BackgroundColor = ConsoleColor.Red;
            _console.ForegroundColor = ConsoleColor.White;
            _console.Error.WriteLine(message);
            _console.ResetColor();
        }
        protected void OutputWarning(string message)
        {
            _console.BackgroundColor = ConsoleColor.Yellow;
            _console.ForegroundColor = ConsoleColor.Black;
            _console.Out.WriteLine(message);
            _console.ResetColor();
        }
    }
}