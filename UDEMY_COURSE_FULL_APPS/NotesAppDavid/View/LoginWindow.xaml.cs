using NotesApp.ViewModel;
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
using System.Windows.Shapes;

namespace NotesApp.View
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            //Asignamos el dataContext desde C# (también se puede hacer por binding en el xaml)
            LoginVM vm = new LoginVM();
            containerGrid.DataContext = vm;
            //Creamos un event handler para el evento HasLoggedIn
            vm.HasLoggedIn += Vm_HasLoggedIn;
          
        }


        private void Vm_HasLoggedIn(object sender, EventArgs e) { 

            //EventHandler
            NotesWindow NW = new NotesWindow();
            NW.Show();
            this.Close();
            
            
        }


        private void haveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            registerStackPanel.Visibility = Visibility.Collapsed;
            loginStackPanel.Visibility = Visibility.Visible;
        }

        private void noAccountButton_Click(object sender, RoutedEventArgs e)
        {
            registerStackPanel.Visibility = Visibility.Visible;
            loginStackPanel.Visibility = Visibility.Collapsed;
        }



    }


}
