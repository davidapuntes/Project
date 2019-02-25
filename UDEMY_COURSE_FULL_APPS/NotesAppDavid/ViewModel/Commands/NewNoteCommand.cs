using NotesApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NotesVM VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public NewNoteCommand(NotesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            //Sólo podremos crear una nueva nota si hay un Notebook seleccionado..
            NotebookModel selectedNotebook = parameter as NotebookModel;
            if (selectedNotebook != null)
                return true;

            return false;
        }

        public void Execute(object parameter)
        {
            //Las notas se han de crear dentro de un notebook... 
            NotebookModel selectedNotebook = parameter as NotebookModel;
            VM.CreateNote(selectedNotebook.Id);
        }
    }
}
