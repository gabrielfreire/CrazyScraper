using CrazyScraper.Commands.Instagram;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrazyScraper
{
    [Command(Name ="crazyscraper", ThrowOnUnexpectedArgument =false, OptionsComparison =System.StringComparison.InvariantCultureIgnoreCase )]
    [VersionOptionFromMember("--version", MemberName =nameof(GetVersion))]
    [Subcommand(typeof(InstagramCmd))]
    public class CrazyScraperCmd : CrazyScraperCmdBase
    {
        public CrazyScraperCmd(IConsole console)
        {
            _console = console;
        }

        protected override Task<int> OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return Task.FromResult(0);
        }
        private static string GetVersion()
            => typeof(CrazyScraperCmd).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}
