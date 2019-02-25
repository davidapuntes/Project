using Microsoft.WindowsAzure.Storage;
using NotesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace NotesApp.View
{
    /// <summary>
    /// Lógica de interacción para NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {

        NotesVM viewModel;

     
        //Add reference to System.Speech (package already installed in .NET, but we have to reference it)
        SpeechRecognitionEngine recognizer;

        public NotesWindow()
        {
            InitializeComponent();

            //Establecemos el dataContext del Dockpanel aquí... Como al NotesVM
            //también estamos accediendo desde la vista, a través de namespace, para no tener duplicados
            //los viewModel, lo que hacemos es acceder al declarado en la vista a través de Resources


            viewModel = this.Resources["vm"] as NotesVM;
            container.DataContext = viewModel;

            viewModel.SelectedNoteChanged += ViewModel_SelectedNoteChanged;


            //Initialize recognizer
            var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
                                  where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                                  select r).FirstOrDefault();
            recognizer = new SpeechRecognitionEngine(currentCulture);

            GrammarBuilder builder = new GrammarBuilder();
            builder.AppendDictation();
            Grammar grammaer = new Grammar(builder);
            recognizer.LoadGrammar(grammaer);


            recognizer.SetInputToDefaultAudioDevice();

            //Linkamos el recognizer con un método que trabajará con el speech analizado
            //Este método (Recognizer_SpeechRecognized()  ) será el event handler que tratará el evento una vez sea  lanzado
            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;

            //Rellenamos los comboBox de tamaño y tipo de letra
            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamilyComboBox.ItemsSource = fontFamilies;

            List<double> fontSizes = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 28, 48, 72 };
            fontSizeComboBox.ItemsSource = fontSizes;
        }

        private async void ViewModel_SelectedNoteChanged(object sender, EventArgs e)
        {
            //Cuando queramos leer la nota, obviamente nos la tenemos que descargar del servidor!!!
            contentRichTextBox.Document.Blocks.Clear();
            if (viewModel.SelectedNote != null) { 
                if (!string.IsNullOrEmpty(viewModel.SelectedNote.FileLocation))
                {
                    Stream rtfFileStream = null;
                    using (HttpClient client = new HttpClient())
                    {
                        var response = await client.GetAsync(viewModel.SelectedNote.FileLocation);
                        rtfFileStream = await response.Content.ReadAsStreamAsync();

                        //Y después cargarla en el RichTextBox....
                        TextRange range = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
                        range.Load(rtfFileStream, DataFormats.Rtf);
                    }
                }
        }
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Esto lo suyo sería hacerlo con un command
        //Actualizará el textbox que hay abajo diciendo la longitud de la nota...
        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ammountCharacters = (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)).Text.Length;

            //Usando formatted string...
            statusTextBlock.Text = $"Document length: {ammountCharacters} characters";
        }


        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            //Aplica negrita cuando actives el botón...Desaplica negrita cuando lo desactives...
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonEnabled)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
        }

        private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Cambia el estado de los botones depende del estado del texto que seleccionemos...
            //Ver como selectedweight/style/decoration, además de tener sus valores normales puede ser unset, de ahí la comprobación extra

            //Si está en negrita el texto seleccionado, automáticamente activa el boldbutton, si no, desactivalo
            var selectedWeight = contentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold));


            //Lo mismo para cursiva y subrayado
            var selectedStyle = contentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            italicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && (selectedStyle.Equals(FontStyles.Italic));

            var selecteDecoration = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlineButton.IsChecked = (selecteDecoration != DependencyProperty.UnsetValue) && (selecteDecoration.Equals(TextDecorations.Underline));

            //También queremos que el tipo y tamaño de letra sea autoreconocido en la selección...(afectando al combobox)
            fontFamilyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontSizeComboBox.Text = (contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();


        }

        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonEnabled)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonEnabled)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
            {
                TextDecorationCollection textDecorations;
                (contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }


        //Event handler for SpeechRecognized
        //Este método está definido como eventHandler en el constructor -->recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;

            //Escribimos en el contentRichTextBox el speech reconocido (la voz)
            //Recordar que una etiqueta <run> es como un <span>
            contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
        }



    
        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            //IsChecked puede ser false/true or null---en caso de null, que sea false tb
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonEnabled)
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple);  //Fire event --> will be treated by the eventHandler (Recognizer_SpeechRecognized()  )
            }
            else
            {
                recognizer.RecognizeAsyncStop();
            }
           
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
            }
        }

        private void fontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.Text);
        }

        private async void saveFileButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = $"{viewModel.SelectedNote.Id}.rtf";
            string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);

            using (FileStream fileStream = new FileStream(rtfFile, FileMode.Create))
            {
                TextRange range = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }

            string fileUrl = await UploadFile(rtfFile, fileName); //Si el blob ha sido correctamente subido, que nos devuelva la url
            viewModel.SelectedNote.FileLocation = fileUrl;//Y esa url será la que guardemos en bbdd para localizar la nota

            viewModel.UpdateSelectedNoteAsync();
        }

        //Ver como retornar valores en métodos async
        private async Task<string> UploadFile(string rtfFileLocation, string fileName)
        {
            string fileUrl = string.Empty;
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=davidnoteclone;AccountKey=ZMDlzCTxhjPersuHhGUeHOJLcxg8hybp8yZK1IjH3IOoe9IInJSv/us15lIIB5MKdv0149lVWe156E/EaJw8Pw==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("notes");
            var blob = container.GetBlockBlobReference(fileName);

            using (FileStream fileStream = new FileStream(rtfFileLocation, FileMode.Open))
            {
                await blob.UploadFromStreamAsync(fileStream);
                fileUrl = blob.Uri.OriginalString;
            }

            return fileUrl;
        }

    }
}
