using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RelayDelegateCommands
{

    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        //En este constructor estamos llamando al constructor padre y pasándole el execute y un predicate null
        public RelayCommand(Action<object> execute) : this(execute, null)
        {

        }

        public event EventHandler CanExecuteChanged
        {
            /*Como sabemos, una vez hagamos el binding en la vista, el comportamiento por defecto de 
             * UpdateSourceTrigger es que lance un cambio en las propiedades nada más perder el foco...Aunque
             nada haya cambiado. Se supone que implementado el requerySuggested, estos lanzamientos de 
             eventos ineficientes son comprobados más exahustivamente....*/


            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //El canExecute dependerá del predicate...Es decir, si no pasamos predicate,
        //se tendrá como un true...y que sí se puede ejecutar... Y, en nuestro caso,
        //pasamos como predicado un MessageBoxCanUse() , ConsoleCanUse(), que devolverán
        //true o false dependiendo de la lógica...
        
         // Así por ejemplo, si canExecute() devuelve false, el botón estará desactivado
         //Si devuelve true, el botón estará activado y al pulsarlo, se invocará el Execute --> _execute(invoke())

        public bool CanExecute(object parameter)
        {
            //In certain situations we may not being using a predicate 
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter); //Lo mismo que _execute.Invoke(parameter)
        }
    }
}
