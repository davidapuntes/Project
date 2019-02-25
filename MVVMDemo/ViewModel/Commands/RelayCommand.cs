using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemo.ViewModel.Commands
{
   
        public class RelayCommand : ICommand
        {

            private readonly Action<object> _execute; //Delegate de acción a ejecutar (es decir, método a ejecutar)
            private readonly Predicate<object> _canExecute; //Método de comprobación para el canExecute

        //Constructor completo en el que existe una comprobación

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        //Constructor para cuando no existe una comprobación (Command sin parámetros)
        public RelayCommand(Action<object> execute) : this(execute, null)
             {
             }
            


            public bool CanExecute(object parameter)
            {
                return _canExecute == null ? true : _canExecute(parameter);
            }

            //Para aumentar la eficacia del EventHandler 
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }


            //Ejecuta delegate ... Es decir, ejecuta el método que me pasen...
            public void Execute(object parameter)
            {
                _execute(parameter);
            }
           
        }
    }


