using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyScraper.Models
{
    public class InstagramUser
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Biography { get; set; }
        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsVerified { get; set; }
        public bool IsBusinessAccount { get; set; }
        public string ExternalUrl { get; set; }
        public string ProfilePicUrl { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"-------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Full name:           {FullName}");
            Console.WriteLine($"Username:            {Username}");
            Console.WriteLine($"Biography:           {Biography}");
            Console.WriteLine($"Followers:           {FollowerCount}");
            Console.WriteLine($"Following:           {FollowingCount}");
            Console.WriteLine($"External URL:        {ExternalUrl}");
            Console.WriteLine($"Is private:          {IsPrivate}");
            Console.WriteLine($"Is verified:         {IsVerified}");
            Console.WriteLine($"Is Business Account: {IsBusinessAccount}");
            Console.WriteLine($"Profile Picture URL: {ProfilePicUrl}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"-------------------------------------");
            Console.ResetColor();
        }

        public static InstagramUser FromHtml(string htmlBody)
        {
            // Make html document node
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlBody);
            var htmlDocument = htmlDoc.DocumentNode;

            // get scripts
            var scripts = htmlDocument.SelectNodes("/html/body/script");
            var offset = "window._sharedData = ";

            // Look for the correct script tag, it is usually the first one
            var script = scripts[0];

            // get correct script tag element and extract text
            var content = script.InnerText;
            var json = content.Substring(offset.Length).Replace(";", "");

            // get dynamic JSON object
            dynamic stuff = JObject.Parse(json);
            dynamic userProfile = stuff.entry_data?.ProfilePage[0]?.graphql?.user;

            // create instagram user object
            var instagramUser = new InstagramUser
            {
                FullName = userProfile.full_name,
                Username = userProfile.username,
                FollowerCount = userProfile.edge_followed_by.count,
                FollowingCount = userProfile.edge_follow.count,
                IsPrivate = userProfile.is_private,
                IsVerified = userProfile.is_verified,
                IsBusinessAccount = userProfile.is_business_account,
                ExternalUrl = userProfile.external_url,
                Biography = userProfile.biography,
                ProfilePicUrl = userProfile.profile_pic_url
            };

            return instagramUser;

        }
    }
}
