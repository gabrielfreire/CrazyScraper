using CrazyScraper.Models;
using CrazyScraper.Utils;
using Newtonsoft.Json;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CrazyScraper.Services
{
    public class InstagramService
    {
        private Page page;
        private Browser browser;
        public async Task Init()
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            page = await browser.NewPageAsync();
        }

        public async Task<InstagramPost> GetPostMetadata(string postUrl)
        {
            InstagramPost post = default;

            try
            {
                await page.GoToAsync(postUrl);
                post = new InstagramPost()
                {
                    Comments = await InstagramScrapingUtils.GetComments(page),
                    Likes = await InstagramScrapingUtils.GetLikes(page),
                    PhotoOrVideoUrls = await InstagramScrapingUtils.GetPhotoOrVideoUrls(page)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return post;
        }

        public async Task SaveFile(InstagramPost data, string fileName)
        {
            var _path = Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}_post.json");
            var _dataStr = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(_path, _dataStr);
        }

        public void Dispose()
        {
            page.Dispose();
            browser.Dispose();
        }
    }
}
