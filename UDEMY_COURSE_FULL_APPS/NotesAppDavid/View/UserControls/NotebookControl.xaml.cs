using NotesApp.Model;
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

namespace NotesApp.View.UserControls
{
    /// <summary>
    /// Lógica de interacción para Notebook.xaml
    /// </summary>
    public partial class NotebookControl : UserControl
    {
        /*
         * Recordar, el NotebookControl, será posicionado en la vista, y recibirá del
         * padre un Notebook (displayedNotebook)....
         * 
         * Cuando lo reciba, entrará en acción el newPropertyMetadata
         * (null --> Valor por defecto
         * SetValues () --> Callback, es decir, el método que se ejecutará al recibir el Notebook del padre...
         * 
         * 
         * Nuestro método setValues lo que hará será pasar el nombre del notebook objeto recibido al
         * campo de texto de nuestro user control....
         * 
         * */


        //propdp
        public NotebookModel DisplayedNotebook
        {
            get { return (NotebookModel)GetValue(DisplayedNotebookProperty); }
            set { SetValue(DisplayedNotebookProperty, value); }
        }

            public static readonly DependencyProperty DisplayedNotebookProperty =
            DependencyProperty.Register("DisplayedNotebook", typeof(NotebookModel), typeof(NotebookControl), new PropertyMetadata(null,SetValues));


        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotebookControl notebook = d as NotebookControl;

            if (notebook != null)
            {
                notebook.notebookNameTextBlock.Text = (e.NewValue as Model.NotebookModel).Name;
            }
        }
        

        public NotebookControl()
        {
            InitializeComponent();
        }
    }
}
