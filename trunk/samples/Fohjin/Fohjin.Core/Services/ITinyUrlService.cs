using System;
using System.IO;
using System.Net;
using FubuMVC.Core;

namespace Fohjin.Core.Services
{
    public interface ITinyUrlService 
    {
        string Tinyfy(string Url);
    }

    public class TinyUrlService : ITinyUrlService
    {
        private const string _tinyUrlUrl = "http://tinyurl.com/api-create.php?url={0}";

        public string Tinyfy(string Url)
        {
            try
            {
                if (Url.Length <= 12)
                {
                    return Url;
                }
                if (!Url.ToLower().StartsWith("http") && !Url.ToLower().StartsWith("ftp"))
                {
                    Url = "http://" + Url;
                }
                var request = WebRequest.Create(_tinyUrlUrl.ToFormat(Url));
                var res = request.GetResponse();
                string text;
                using (var reader = new StreamReader(res.GetResponseStream()))
                {
                    text = reader.ReadToEnd();
                }
                return text;
            }
            catch (Exception)
            {
                return Url;
            }
        }
    }
}