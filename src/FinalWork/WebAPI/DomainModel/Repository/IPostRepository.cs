using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Repository
{
    public interface IPostRepository : IRepository<Post, string>
    {
    }
}
