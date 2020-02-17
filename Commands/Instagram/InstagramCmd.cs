using CrazyScraper.Models;
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
    //... instagram
    [Command(Name ="instagram", Description ="Scrape an Instagram Profile" )]
    public class InstagramCmd : CommandBase
    {
        // -p gabrielfreiredev
        [Option(CommandOptionType.MultipleValue, ShortName = "p", LongName = "profiles", Description = "Instagram Profile Name", ValueName = "profiles", ShowInHelpText = true)]
        public List<string> ProfilesNames { get; set; }

        // -o file.json
        [Option(CommandOptionType.SingleValue, ShortName = "o", LongName = "output", Description = "Output file name", ValueName = "output", ShowInHelpText = true)]
        public string OutputFile { get; set; }

        public InstagramCmd()
        {
        }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            
            if(ProfilesNames.Count == 0)
            {
                OutputError("Please provide at least one profile name");
                app.ShowHelp();
            }

            if (!string.IsNullOrEmpty(OutputFile))
            {
                OutputToConsole($"File name {OutputFile}\n");
                OutputWarning("Sorry, output to file is not yet available :(\n");
            }

            // create a task list
            var taskList = new List<Task<InstagramUser>>();

            OutputToConsole($"Scraping...\n");
            foreach (var profileName in ProfilesNames)
            {
                taskList.Add(ScrapeAsync(profileName));
            }

            // run all of them asynchronously
            var instagramUsers = await Task.WhenAll(taskList);
            OutputToConsole($"Done.\n");

            // display results
            foreach (var instagramUser in instagramUsers)
            {
                if (instagramUser != null)
                {
                    instagramUser.Show();
                }
            }

            return 1;
        }

        private async Task<InstagramUser> ScrapeAsync(string ProfileName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://www.instagram.com/{ProfileName}");
                    if (response.IsSuccessStatusCode)
                    {
                        var htmlBody = await response.Content.ReadAsStringAsync();
                        return InstagramUser.FromHtml(htmlBody);
                    }
                    OutputError($"Profile {ProfileName} doesn't exist");
                }
                return null;
            }
            catch (Exception ex)
            {
                OutputError($"{ProfileName} is not a valid user or something else happened: {ex.Message}");
                return null;
            }
        }

    }
}
