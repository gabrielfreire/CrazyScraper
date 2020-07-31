using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrazyScraper.Models
{
    public class InstagramPost
    {
        public List<string> Comments { get; set; }
        public int CommentsCount => Comments.Count;
        public List<string> PhotoOrVideoUrls { get; set; }
        public long Likes { get; set; }
        public string Caption => Comments.First();
        public InstagramPost()
        {
            PhotoOrVideoUrls = new List<string>();
            Comments = new List<string>();
        }
    }
}
