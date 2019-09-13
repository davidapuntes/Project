using Presupuestea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presupuestea.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Presupuestea.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	    public partial class ProfessionalSignUp : ContentPage
        {
            public ProfessionalSignUp()
            {
                InitializeComponent();
            }

            private async void LoginButtonClicked(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new ProfessionalLogIn());
            }

            async void SignUpButtonClicked(object sender, EventArgs e)
            {
                var user = new User
                {
                    Username = UsernameEntry.Text,
                    Password = PasswordEntry.Text,
                    Email = EmailEntry.Text
                };
                var signUpSucceeded = AreDetailsValid(user);
                if (signUpSucceeded)
                {
                    var rootpage = Navigation.NavigationStack.FirstOrDefault();
                    if (rootpage != null)
                    {
                        App.IsUserLoggedIn = true;
                        Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
                        await Navigation.PopToRootAsync();
                    }
                    else
                    {
                        errortext.Text = "SignUpError";
                    }
                }

            }

            bool AreDetailsValid(User user)
            {
                return (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@"));
            }
        }
    }

