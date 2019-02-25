using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelayDelegateCommands.ViewModels
{
    //www.toskerscorner.com
    public class MessageViewModel
    {
        //Esto es una lista de WPF, que permitirá la actualización de la vista si los elementos cambian...
        public ObservableCollection<string> MyMessages { get; private set; }

        public RelayCommand MessageBoxCommand { get; private set; }
        public RelayCommand ConsoleLogCommand { get; private set; }

        public MessageViewModel()
        {
            MyMessages = new ObservableCollection<string>()
            {
                "Hello World!",
                "My name is Joe",
                "I love learning commands",
                "Im a message box",
                "Im a console"
            };

            MessageBoxCommand = new RelayCommand(DisplayInMessageBox, MessageBoxCanUse);
            ConsoleLogCommand = new RelayCommand(DisplayInConsole, ConsoleCanUse);
        }

        public void DisplayInMessageBox(object message)
        {
            MessageBox.Show((string)message);
        }

        public void DisplayInConsole(object message)
        {
            Console.WriteLine((string)message);
        }




        //Estos métodos son los que usaremos como predicate en el constructor del RelayCommand
        public bool MessageBoxCanUse(object message)
        {
            if ((string)message == "Im a console")
                return false;

            return true;
        }

        public bool ConsoleCanUse(object message)
        {
            if ((string)message == "Im a message box")
                return false;

            return true;
        }
    }
}
