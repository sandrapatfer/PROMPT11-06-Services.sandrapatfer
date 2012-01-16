using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.IO;
using DomainModel;

namespace Service
{
    abstract class HtmlMediaTypeFormatter : MediaTypeFormatter
    {
        public HtmlMediaTypeFormatter()
        {
            base.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        protected override object OnReadFromStream(Type type, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders)
        {
            throw new NotImplementedException();
        }

        protected override bool CanReadType(Type type)
        {
            return false;
        }
    }

    class PostHtmlMediaTypeFormatter : HtmlMediaTypeFormatter
    {
        protected override void OnWriteToStream(Type type, object value, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders, System.Net.TransportContext context)
        {
            var post = value as Post;
            using (var sw = new StreamWriter(stream))
            {
                sw.WriteLine("<html><body>");
                sw.WriteLine("<h1>" + post.Title + "</h1>");
                sw.WriteLine("<h2>Last modified at: " + post.LastUpdatedTime.ToString() + "</h2>");
                sw.WriteLine("<p>" + post.Content + "</p>");
                sw.WriteLine("</body></html>");
            }
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(Post);
        }
    }

    class PostListHtmlMediaTypeFormatter : HtmlMediaTypeFormatter
    {
        protected override void OnWriteToStream(Type type, object value, System.IO.Stream stream, System.Net.Http.Headers.HttpContentHeaders contentHeaders, System.Net.TransportContext context)
        {
            var list = value as List<Post>;
            using (var sw = new StreamWriter(stream))
            {
                sw.WriteLine("<html><body>");
                if (list != null && list.Count > 0)
                {
                    sw.WriteLine("<h1>List of posts</h1>");
                    foreach (var post in list)
                    {
                        sw.WriteLine("<section>");
                        sw.WriteLine("<h1>" + post.Title + "</h1>");
                        sw.WriteLine("<p>" + post.Content + "</p>");
                        sw.WriteLine("</section>");
                    }
                }
                sw.WriteLine("</body></html>");
            }
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(List<Post>);
        }
    }
}
