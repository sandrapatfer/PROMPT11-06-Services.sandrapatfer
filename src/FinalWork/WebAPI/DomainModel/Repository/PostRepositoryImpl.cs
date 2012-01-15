using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Repository
{
    public class PostRepositoryImpl : IPostRepository
    {
        private RavenDBRepository<Post> _repository = new RavenDBRepository<Post>();

        public IQueryable<Post> GetAll()
        {
            return _repository.GetAll();
        }

        public Post Get(string entityId)
        {
            if (!entityId.StartsWith("posts"))
            {
                entityId = string.Format("posts/{0}", entityId);
            }
            return _repository.Get(entityId);
        }

        public void Add(Post entity)
        {
            _repository.Create(entity);
        }

        public void Update(Post entity)
        {
            _repository.Update(entity);
        }

        public void Delete(string entityId)
        {
            if (!entityId.StartsWith("posts"))
            {
                entityId = string.Format("posts/{0}", entityId);
            }
            _repository.Delete(entityId);
        }
    }
}
