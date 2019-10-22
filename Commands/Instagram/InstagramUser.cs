using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using System;

namespace CrazyScraper.Commands.Instagram
{
    internal class InstagramUser
    {
        public string id { get; set; }
        public string biography { get; set; }
        public string external_url { get; set; }
        public int follower_count { get; set; }
        public int follow_count { get; set; }
        public string full_name { get; set; }
        public string username { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void Show(IConsole console)
        {
            console.BackgroundColor = ConsoleColor.Black;
            console.ForegroundColor = ConsoleColor.Blue;
            console.Out.WriteLine("-----------------------------");
            console.ForegroundColor = ConsoleColor.Green;
            console.Out.WriteLine($"Name: {full_name}");
            console.Out.WriteLine($"Followers: {follower_count}");
            console.Out.WriteLine($"Following: {follow_count}");
            console.Out.WriteLine($"Username: @{username}");
            console.ForegroundColor = ConsoleColor.Blue;
            console.Out.WriteLine("-----------------------------");
            console.ResetColor();
        }
    }
}