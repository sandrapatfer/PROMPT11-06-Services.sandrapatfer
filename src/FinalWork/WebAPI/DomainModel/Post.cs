using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DomainModel
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public string Content { get; set; }
    }
}
