using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Repository;

namespace DomainModel
{
    public class PostManagerImpl : IPostManager
    {
        private IPostRepository _repository;

        public PostManagerImpl(IPostRepository repo)
        {
            _repository = repo;
//            var post = new Post() { Title = "primeiro post" };
//            Create(post);
        }

        #region IPostManager Members

        public List<Post> GetAllPosts()
        {
            return _repository.GetAll().ToList();
        }

        public Post Get(string id)
        {
            return _repository.Get(id);
        }

        public void Create(Post post)
        {
            _repository.Add(post);
        }

        public void Update(Post post)
        {
            _repository.Update(post);
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        #endregion

    }
}
