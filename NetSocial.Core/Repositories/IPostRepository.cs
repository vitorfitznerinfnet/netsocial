using System.Collections.Generic;

namespace NetSocial.Core
{
    public interface IPostRepository
    {
        void Save(Post post);
        IEnumerable<Post> GetAll();
    }
}
