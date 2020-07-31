using CrazyScraper.Services;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrazyScraper.Commands.Instagram
{
    [Command(Name = "post", Description = "Get instagram post comments")]
    public class PostSubcommand : BaseCommand
    {
        private InstagramService _instagramService;
        [Option(CommandOptionType.SingleValue, ShortName = "u", LongName = "url", Description = "Instagram post url", ValueName = "url", ShowInHelpText = true)]
        public string PostUrl { get; set; }
        public PostSubcommand(InstagramService instagramService)
        {
            _instagramService = instagramService;
        }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            await _instagramService.Init();

            var _post = await _instagramService.GetPostMetadata(PostUrl);

            await _instagramService.SaveFile(_post, "_0tests");

            _instagramService.Dispose();

            return 1;
        }
    }
}
