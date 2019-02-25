using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NotesApp
{
    
    public partial class App : Application
    {
        //Variable global a evaluar desde NotesWindow -- 
        public static string UserId = string.Empty;
        //Configuramos conexión con el backend de azure
        public static MobileServiceClient MobileServiceClient = new MobileServiceClient("https://davidevernote.azurewebsites.net");
    }
}
