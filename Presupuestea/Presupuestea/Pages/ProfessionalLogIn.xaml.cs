using Presupuestea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Presupuestea.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfessionalLogin : ContentPage
    {
        public ProfessionalLogin()
        {
            InitializeComponent();
            // BindingContext = new ContentPageViewModel();  
        }

        async void SigUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfessionalSignUp());


        }

        async void Login_Clicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = UserNameEntry.Text,
                Password = PasswordEntry.Text
            };

            var isVaild = AreCredentialsCorrect(user);
            if (isVaild)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                LoginFaild.Text = "Contraseña incorrecta";
                PasswordEntry.Text = string.Empty;
            }


        }

        bool AreCredentialsCorrect(User user)
        {
            return user.Username == Constants.Username && user.Password == Constants.Password;

        }
    }




}