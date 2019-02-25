using NotesApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public LoginVM VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(LoginVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            ////Si no todos los campos están rellenos, el botón registrar estará deshabilitado
            //var user = parameter as UserModel;

            //if (user != null)
            //{
            //    if (string.IsNullOrEmpty(user.Username))
            //        return false;
            //    if (string.IsNullOrEmpty(user.Password))
            //        return false;
            //    if (string.IsNullOrEmpty(user.Email))
            //        return false;
            //    if (string.IsNullOrEmpty(user.Lastname))
            //        return false;
            //    if (string.IsNullOrEmpty(user.Name))
            //        return false;
            //    return true;
            //}

            //return false;

            return true;
        }

        public void Execute(object parameter)
        {
            VM.RegisterAsync();
        }
    }
}
