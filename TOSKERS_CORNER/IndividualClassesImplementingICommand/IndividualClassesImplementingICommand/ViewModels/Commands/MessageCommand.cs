using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IndividualClassesImplementingICommand.ViewModels.Commands
{
    public class MessageCommand : ICommand
    {

        private Action _execute;

        /* Una Action es muy parecido a un delegate. Guardará un método... Es decir, por ejemplo,
         * en este constructor, nosotros podríamos pasar como parámetro un método*/
        public MessageCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;
        

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(); 
        }
    }
}
