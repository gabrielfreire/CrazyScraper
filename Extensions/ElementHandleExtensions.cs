using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrazyScraper.Extensions
{
    public static class ElementHandleExtensions
    {
        public static async Task<string> TextContentAsync(this ElementHandle element)
        {
            var _prop = await element.GetPropertyAsync("innerText");
            return await _prop.JsonValueAsync<string>();
        }
        public static async Task<string> SrcAsync(this ElementHandle element)
        {
            var _prop = await element.GetPropertyAsync("src");
            return await _prop.JsonValueAsync<string>();
        }
    }
}
