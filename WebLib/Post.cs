using System;

namespace WebLib
{
    public class Post
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public string PublishDate { get; } = DateTime.Now.Date.ToLongDateString();
        public bool IsDraft { get; set; }
        public string Keywords { get; set; }
        public string Category { get; set; }
    }
}