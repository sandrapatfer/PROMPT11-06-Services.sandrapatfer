using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Document;

namespace DomainModel.Repository
{
    internal class RavenDBRepository<T>
    {
        private DocumentStore _store;

        public RavenDBRepository()
        {
            _store = new DocumentStore() { Url = "http://localhost:8080" };
            _store.Initialize();
        }

        public IQueryable<T> GetAll()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<T>();
            }
        }

        public T Get(string id)
        {
            using (var session = _store.OpenSession())
            {
                return session.Load<T>(id);
            }
        }

        public void Create(T item)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(item);
                session.SaveChanges();
            }
        }

        public void Update(T item)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(item); 
                session.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var session = _store.OpenSession())
            {
                var item = session.Load<T>(id);
                if (item != null)
                {
                    session.Delete(item);
                    session.SaveChanges();
                }
            }
        }
    }
}
    