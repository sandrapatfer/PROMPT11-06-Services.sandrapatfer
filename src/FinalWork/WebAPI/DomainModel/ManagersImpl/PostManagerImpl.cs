using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class PostManagerImpl : IPostManager
    {
        // TODO set the repository
        List<Post> _tmpPosts = new List<Post>();

        public PostManagerImpl()
        {
            _tmpPosts.Add(new Post() { Title = "um post" });
            _tmpPosts.Add(new Post() { Title = "outro post" });
        }

        #region IPostManager Members

        public List<Post> GetAllPosts()
        {
            return _tmpPosts;
        }

        #endregion
    }
}
