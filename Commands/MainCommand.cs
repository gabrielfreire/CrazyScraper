using CrazyScraper.Commands.Instagram;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrazyScraper.Commands
{
    [Command(Name ="crazyscraper", OptionsComparison =System.StringComparison.InvariantCultureIgnoreCase )]
    [VersionOptionFromMember("--version", MemberName =nameof(GetVersion))]
    [Subcommand(typeof(InstagramCmd))]
    public class MainCommand : BaseCommand
    {

        protected override Task<int> OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return Task.FromResult(0);
        }
        private static string GetVersion()
            => typeof(MainCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}
