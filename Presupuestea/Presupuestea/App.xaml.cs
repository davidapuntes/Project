﻿using Presupuestea.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Presupuestea
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public App()
        {
            InitializeComponent();
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new ProfessionalLogin());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts  
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps  
        }

        protected override void OnResume()
        {
            // Handle when your app resumes  
        }
    }
}
