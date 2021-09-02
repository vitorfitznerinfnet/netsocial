using System;
using System.Collections.Generic;

namespace NetSocial.Core
{
    public class Aplication
    {
        private readonly IPostRepository postRepository;

        public Aplication(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public void CriarPost(string title, string text, string img)
        {
            var post = new Post();
            post.Id = Guid.NewGuid();
            post.Title = title;
            post.Text = text;
            post.Img = img;
            post.CreatedAt = DateTime.UtcNow;

            postRepository.Save(post);
        }

        public IEnumerable<Post> ListPosts()
        {
            return postRepository.GetAll();
        }
    }
}
