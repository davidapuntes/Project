using LandmarkAI.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //1ª parte pipe--> Infomración sobre el filtro que se mostrará al user
            //2ª parte pipe--> Filtro por extensión
            //Se le dan 2 opciones para que elijan.....
            dialog.Filter = "Image files (*.png; *.jpg)|*.png;*.jpg;*jpeg|All files (*.*)|*.*";
            //Directorio por defecto donde empezará a buscar
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            //If the user has successfully selected a file...s
            if(dialog.ShowDialog()== true)
            {
                string fileName = dialog.FileName;
                selectedImage.Source = new BitmapImage(new Uri(fileName));

                MakePredictionAsync(fileName);
            }
        }

        private async void MakePredictionAsync(string fileName)
        {
            //Estos datos los cogemos de la info de la api rest...
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.1/Prediction/bf39d301-3888-43cf-91fe-1509ce5ac26a/image?iterationId=2cc8d120-36d9-417c-b3c3-213b910a3f20";
            string prediction_key = "fd63926c323344a0aacaa249ebd73fc6";
            string content_type = "application/octet-stream";
            var file = File.ReadAllBytes(fileName); //Convert file to an array of Bytes with all the info

            //Usar este tipo de inyección para que maneje los recursos, conexiones el sólo... (cierre la conexión automáticamente, etc..)
            using (HttpClient client = new HttpClient())
            {
                /*
                    Prediction-Key es una propiedad que la api ha creado para el header
                    Content-type es una propiedad que por defecto del header, directamente asociada al contenido de la request...
                */
                client.DefaultRequestHeaders.Add("Prediction-Key", prediction_key);

                //Establecemos el contenido de la request que enviaremos....
                using (var content = new ByteArrayContent(file))
                {
                    //Establecemos la propiedad contentType del header desde el contenido directamente
                    //Recordar que en postman, al llenar el contenido, por defecto ya se rellenaba sólo la propiedad content-type del header
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(content_type);


                    /*
                    * Para consumir rest-apis:
                       * putAsync()
                       *getAsync()
                       *postAsync()...
                                                       
                     */
                    var response = await client.PostAsync(url, content); //Hago un post pero guardo la respuesta....

                    var responseString = await response.Content.ReadAsStringAsync(); //Leo el contenido de la respuesta...Recibiré un object, en JSON

                    //Convertiremos el JSON a un objeto de clase C# --> Podemos ir a jsonutils.com y convertir el JSON en una clase C#
                    //También estamos usando el paquete newtonSoft.json (References---> Manage nuget packages)         
                                                   
                    List<Prediction> predictions = (JsonConvert.DeserializeObject<CustomVision>(responseString)).Predictions;
                    predictionsListView.ItemsSource = predictions;
                }
            }
        }
    }
}
