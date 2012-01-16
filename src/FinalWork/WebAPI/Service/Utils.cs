using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace Service
{
    public static class Utils
    {
        public static bool AcceptsAll(this HttpRequestMessage request)
        {
            return request.Headers.Accept.Any(h => h.MediaType == "*/*");
        }
        public static bool AcceptsHtml(this HttpRequestMessage request)
        {
            return request.Headers.Accept.Any(h => h.MediaType == "text/html");
        }
        public static bool AcceptsAtom(this HttpRequestMessage request)
        {
            return request.Headers.Accept.Any(h => h.MediaType == "application/atom+xml");
        }
        public static bool AcceptsRss(this HttpRequestMessage request)
        {
            return request.Headers.Accept.Any(h => h.MediaType == "application/rss+xml");
        }

        public static string RemoveFromEnd(this string str, string remove)
        {
            if (str.EndsWith(remove))
            {
                return str.Substring(0, str.Length - remove.Length);
            }
            return str;
        }

        public static string BaseUrl(this HttpRequestMessage request, string requestSpecific)
        {
            return request.RequestUri.Scheme + "://" + request.RequestUri.Authority +
                request.RequestUri.LocalPath.TrimEnd('/').RemoveFromEnd(requestSpecific).TrimEnd('/');
        }
    }
}