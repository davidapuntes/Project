using RssReader.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RssReader.ViewModel
{
    public class RssHelper : IRssHelper
    {
        public List<Item> GetPosts()
        {
            List<Item> posts = new List<Item>();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(FinZenBlog));

            using (WebClient client = new WebClient())
            {
                string xml = Encoding.Default.GetString(client.DownloadData("https://www.finzen.mx/blog-feed.xml"));

                using (Stream reader = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    FinZenBlog blog = (FinZenBlog)xmlSerializer.Deserialize(reader);

                    posts = blog.Channel.Item;
                }
            }

            return posts;
        }
    }
}