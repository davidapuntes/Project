using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RssReader.Model
{
    public class CData
    {
        public string ActualString { get; set; }
    }

    [XmlRoot(ElementName = "item")] ////XMLRoot meaning that it has elements inside....
    public class Item
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
        private string pubDate;
        [XmlElement(ElementName = "pubDate")]
        public string PubDate
        {
            get { return pubDate; }
            set
            {
                pubDate = value;
                PublishedDate = DateTime.ParseExact(pubDate, "ddd, dd MMM yyyy HH:mm:ss GMT", CultureInfo.InvariantCulture);
            }
        }

        public DateTime PublishedDate { get; set; }

        /*Cuidado, el elemento creator está asociado en el xml a un espacio de nombres
        <dc:creator>Eduardo Rosas</dc:creator>
        <rss xmlns:dc="http//purl.org/dc/elements/1.1/" */


        [XmlElement(ElementName = "creator", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string Creator { get; set; }
    }

    [XmlRoot(ElementName = "channel")] //XMLRoot meaning that it has elements inside....
    public class Channel
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Item { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
    }

    //XMLRoot meaning that it has elements inside....
    [XmlRoot(ElementName = "rss")] //The element rss in xml will be converted to FinzenBlog object in c#
    public class FinZenBlog
    {
        [XmlElement(ElementName = "channel")] //The element in xml is called channel
        public Channel Channel { get; set; }
    }
}
