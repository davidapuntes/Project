using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ViewModel
{
    //Recordar, en los ViewModel es donde creamos/exponemos los objetos que serán linkados/bound con la vista

    /* In the notes Window we will have to list Notebooks, Notes, Display text for each note, commands for creating notebooks,
     * creating note,.... 
     * */
    public class NotesVM : INotifyPropertyChanged
    {
        //Property for showing or not the editing textbox
        //Esta propiedad esta linkada en la vista...Por lo que, tenemos que :
         //Declarar INotifyPropertyChanged
         //Hacerla una fullProp
         //Lanzar el evento en el setter para que la vista se entere


        private bool isEditing;

        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                isEditing = value;
                OnPropertyChanged("IsEditing");
            }
        }


        private void OnPropertyChanged(string propertyName)
        { 
            //Para lanzar evento de cambio de propiedad -> INotifyPropertyChaned
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private NoteModel note;

        public NoteModel SelectedNote
        {
            get { return note; }
            set
            {
                note = value;
                //Cuando leamos una nota, lo que hay que hacer es mostrar el texto que hay guardado en el richTextBox
                SelectedNoteChanged(this, new EventArgs()); //Fire Event !! Manejado por el eventHandler
                OnPropertyChanged("SelectedNote");

            }
        }



        public ObservableCollection<NotebookModel> Notebooks { get; set; }
        public BeginEditCommand BeginEditCommand { get; set; }
        public HasEditedCommand HasEditedCommand { get; set; }
        public event EventHandler SelectedNoteChanged; //EventHandler para ver los cambios de SelectedNote. Este event
        //Handler será definido en NotesWindows.xaml.cs....

        private NotebookModel selectedNotebook;

        public event PropertyChangedEventHandler PropertyChanged;

        public NotebookModel SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                //Cuando establezcamos el notebook elegido (setter) también tenemos que cargar sus notas...
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook"); 
                ReadNotes();
            }
        }

        public ObservableCollection<NoteModel> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public NotesVM()
        {

            IsEditing = false; //ContextMenu boolean controller

            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            BeginEditCommand = new BeginEditCommand(this);
            HasEditedCommand = new HasEditedCommand(this);

            //Inicializamos listas
            Notebooks = new ObservableCollection<NotebookModel>();
            Notes = new ObservableCollection<NoteModel>();

            //Y leemos nada más empezar de la bbdd
            ReadNotebooks();
            ReadNotes();

        }


        public async void ReadNotes()
        {
            //using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            //{
            //    if (SelectedNotebook != null)
            //    {
            //        conn.CreateTable<NoteModel>(); //Make sure the table exist
            //        //Que sólo muestre las notas del notebook seleccionado
            //        var notes = conn.Table<NoteModel>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

            //        Notes.Clear();
            //        foreach (var note in notes)
            //        {
            //            Notes.Add(note);
            //        }
            //    }
            //}



           
                try
                {
                    var notes = await App.MobileServiceClient.GetTable<NoteModel>().Where(n => n.NotebookId == SelectedNotebook.Id).ToListAsync();

                    Notes.Clear();
                    foreach (var note in notes)
                    {
                        Notes.Add(note);
                    }
                }
                catch (Exception ex)
                {

                }
            
           
        }



        public async void CreateNotebook()
        {
            NotebookModel newNotebook = new NotebookModel()
            {
                Name = "New notebook",
                UserId =App.UserId
            };

            //DatabaseHelper.Insert(newNotebook);

            try
            {
                await App.MobileServiceClient.GetTable<NotebookModel>().InsertAsync(newNotebook);
            }
            catch (Exception ex)
            {

            }
            ReadNotebooks();
        }

        public async void ReadNotebooks() {
            //{
            //    using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            //    {
            //        conn.CreateTable<NotebookModel>(); //Make sure the table exists...
            //        var notebooks = conn.Table<NotebookModel>().ToList();

            //        Notebooks.Clear();
            //        foreach (var notebook in notebooks)
            //        {
            //            Notebooks.Add(notebook);
            //        }
            //    }


            try
            {
                //Ver que ahora ya sólo leemos los notebooks del usuario que se ha logado...
                var notebooks = await App.MobileServiceClient.GetTable<NotebookModel>().Where(n => n.UserId == App.UserId).ToListAsync();

                Notebooks.Clear();
                foreach (var notebook in notebooks)
                {
                    Notebooks.Add(notebook);
                }

                //selectedNotebook= Notebooks.FirstOrDefault(); //weird behavbour
            }
            catch (Exception ex)
            {

            }


        }


        public async void CreateNote(string notebookId)
        {
            NoteModel newNote = new NoteModel()
            {
                NotebookId = notebookId,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = "New note"
            };

            //  DatabaseHelper.Insert(newNote); //Los métodos del DatabaseHelper eran todos estáticos
            /*Al pasar un Note de parámetros, el método genérico reconocerá con qué tiene
            que operar....*/


            try
            {
                await App.MobileServiceClient.GetTable<NoteModel>().InsertAsync(newNote);
            }
            catch (Exception ex)
            {

            }



            ReadNotes();
        }

        public void StartEditing()
        {   //Linkado con el command que hará visible el context menu ..BeginEditCommand
            IsEditing = true;
        }

        public async void HasRenamed(NotebookModel notebook)
        {
            //Linkado con el command al perder el foco del textbox..Lo que significará que se ha terminado de editar
            //y se puede actualizar el nombre del Notebook...
            if (notebook != null)
            {
                //// DatabaseHelper.Update(notebook);
                try
                {
                    await App.MobileServiceClient.GetTable<NotebookModel>().UpdateAsync(notebook);
                    IsEditing = false;
                    ReadNotebooks();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task UpdateSelectedNoteAsync()
        {
            // DatabaseHelper.Update(SelectedNote);
            try
            {
                await App.MobileServiceClient.GetTable<NoteModel>().UpdateAsync(SelectedNote);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
