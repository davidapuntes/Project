using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UsingINotifyPropertyChanged
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _main = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();            
            //La info behind the MainWindow será por tanto el MainViewModel y ya podremos
            //y ya podremos hacer binding en la vista con sus objetos, propiedades...
            DataContext = _main;
        }

        private void Red_Clicked(object sender, RoutedEventArgs e)
        {
            _main.SetBackground(Brushes.Red);
        }

        private void Blue_Clicked(object sender, RoutedEventArgs e)
        {
            _main.SetBackground(Brushes.Blue);
        }

        private void Yellow_Clicked(object sender, RoutedEventArgs e)
        {
            _main.SetBackground(Brushes.Yellow);
        }
    }
}
