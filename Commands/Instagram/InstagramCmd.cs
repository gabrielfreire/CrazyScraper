using HtmlAgilityPack;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrazyScraper.Commands.Instagram
{
    [Command(Name ="instagram", Description ="Scrape an Instagram Profile" )]
    [Subcommand(typeof(PostSubcommand))]
    public class InstagramCmd : BaseCommand
    {
        [Option(CommandOptionType.MultipleValue, ShortName = "p", LongName = "profiles", Description = "Instagram Profile Name", ValueName = "profiles", ShowInHelpText = true)]
        public List<string> ProfilesName { get; set; }


        private async IAsyncEnumerable<string> GetData(List<string> profilesName)
        {
            foreach(var name in profilesName)
            {
                await Task.Delay(0);
                yield return name;
            }
        }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            var profileNamesAsyncEnumerable = GetData(ProfilesName);
            
            if(await profileNamesAsyncEnumerable.IsEmptyAsync())
            {
                OutputError("Please provide at least one profile name");
                app.ShowHelp();
            }

            // create a task list
            var taskList = new List<Task>();

            await foreach (var profileName in profileNamesAsyncEnumerable)
            {
                taskList.Add(ScrapeAsync(profileName));
            }

            // run all of them asynchronously
            await Task.WhenAll(taskList);

            return 1;
        }

        private async Task ScrapeAsync(string ProfileName)
        {
            OutputToConsole($"Scraping Instagram Page of {ProfileName}...\n");

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://www.instagram.com/{ProfileName}");
                    if (response.IsSuccessStatusCode)
                    {
                        var htmlBody = await response.Content.ReadAsStringAsync();
                        var instagramUser = ParseInstagramHtml(htmlBody);
                        if (instagramUser != null)
                        {
                           // instagramUser.Show(Console);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OutputError($"{ProfileName} is not a valid user or something else happened: {ex.Message}");
            }
        }

        private InstagramUser ParseInstagramHtml(string htmlBody)
        {
            // Make html document node
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlBody);
            var htmlDocument = htmlDoc.DocumentNode;
            
            // get scripts
            var scripts = htmlDocument.SelectNodes("/html/body/script");
            var offset  = "window._sharedData = ";
            
            // Look for the correct script tag, it is usually the first one
            var script  = scripts[0];

            // get correct script tag element and extract text
            var content = script.InnerText;
            var json    = content.Substring(offset.Length).Replace(";", "");

            // get dynamic JSON object
            dynamic stuff           = JObject.Parse(json);
            dynamic userProfilePage = stuff.entry_data?.ProfilePage[0]?.graphql?.user;

            // create instagram user object
            var instagramUser = new InstagramUser
            {
                biography      = userProfilePage.biography,
                username       = userProfilePage.username,
                full_name      = userProfilePage.full_name,
                external_url   = userProfilePage.external_url,
                id             = userProfilePage.id,
                follower_count = userProfilePage.edge_followed_by.count,
                follow_count   = userProfilePage.edge_follow.count,
            };

            return instagramUser;
        }
    }
}
