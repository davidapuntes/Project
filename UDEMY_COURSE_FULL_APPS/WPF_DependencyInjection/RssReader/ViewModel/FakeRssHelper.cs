using RssReader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ViewModel
{
    public class FakeRssHelper : IRssHelper
    {
        public List<Item> GetPosts()
        {
            List<Item> items = new List<Item>();

            items.Add(new Item()
            {
                Title = "Some example of a very long title to test when designing the application with some fake example data",
                PubDate = "Thu, 22 Nov 2018 06:13:30 GMT"
            });

            items.Add(new Item()
            {
                Title = "",
                PubDate = "Fri, 23 Nov 2018 09:27:14 GMT"
            });

            return items;
        }
    }
}
