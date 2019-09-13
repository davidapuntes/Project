using Presupuestea.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Presupuestea
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnLogout_Clicked(object sender, EventArgs e)
        {

            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new ProfessionalLogin(), this);
            await Navigation.PopAsync();

        }
    }
}
