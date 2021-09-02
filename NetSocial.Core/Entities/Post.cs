using System;

namespace NetSocial.Core
{
    public class Post
    {
        public Guid Id { get; internal set; }
        public string Title { get; internal set; }
        public string Text { get; internal set; }
        public string Img { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
    }
}
