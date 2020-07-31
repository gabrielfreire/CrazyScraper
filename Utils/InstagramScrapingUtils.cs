using CrazyScraper.Extensions;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrazyScraper.Utils
{
    public static class InstagramScrapingUtils
    {
        public static async Task<List<string>> GetComments(Page page)
        {
            var _comments = new List<string>();
            var _commentSelector = ".C4VMK span";
            try
            {
                await LoadMoreComments(page);
                var _commentElements = await page.QuerySelectorAllAsync(_commentSelector);
                if (_commentElements != null)
                {
                    foreach(var comment in _commentElements)
                    {
                        var _text = await comment.TextContentAsync();
                        _comments.Add(_text);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }

            return _comments;
        }

        public static async Task LoadMoreComments(Page page)
        {
            try
            {
                var _moreButtonSelector = "button.dCJp8";
                var _button = await page.WaitForSelectorAsync(_moreButtonSelector, new WaitForSelectorOptions() { Timeout = 3000 });
                if (_button != null)
                {
                    await _button.ClickAsync();
                    await LoadMoreComments(page);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public static async Task<long> GetLikes(Page page)
        {
            long likes = default;
            try
            {
                var _likesSelector = "div.Nm9Fw button.sqdOP span";
                var _likesElement = await page.QuerySelectorAsync(_likesSelector);
                if (_likesElement != null)
                {
                    var _text = await _likesElement.TextContentAsync();
                    if (!string.IsNullOrEmpty(_text))
                        likes = long.Parse(_text.Replace(",", ""));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return likes;
        }

        public static async Task<List<string>> GetPhotoOrVideoUrls(Page page)
        {
            var _urls = new List<string>();
            try
            {
                var _selector = "div._97aPb img.FFVAD";
                var _elements = await page.QuerySelectorAllAsync(_selector);
                if (_elements != null)
                {
                    foreach(var el in _elements)
                    {
                        var _url = await el.SrcAsync();
                        _urls.Add(_url);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _urls;
        }
    }
}
