using RssReader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ViewModel
{
    public class MainVM
    {
        IRssHelper rssHelper;

        public ObservableCollection<Item> Items { get; set; }

        public MainVM(IRssHelper rssHelper)
        {
            /*Note like the constructor receive an interface type
            *The kind of instance (implementing that interface) will be resolved in MainWindow.xaml.cs that is where
            *the MainVM is first called or resolved like a dependency...And will be resolved depending on 
            * the kind of type registered at app.xaml.cs*/
            this.rssHelper = rssHelper;

            Items = new ObservableCollection<Item>();

            ReadRss();
        }

        private void ReadRss()
        {
            var posts = rssHelper.GetPosts();

            Items.Clear();

            foreach (var post in posts)
            {
                Items.Add(post);
            }
        }
    }
}