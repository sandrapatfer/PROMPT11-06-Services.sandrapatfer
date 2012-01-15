using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public interface IPostManager
    {
        List<Post> GetAllPosts();

        Post Get(string id);
        
        void Create(Post post);

        void Update(Post post);

        void Delete(string id);
    }
}
