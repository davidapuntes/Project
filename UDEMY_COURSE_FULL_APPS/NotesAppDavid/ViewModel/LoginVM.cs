using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ViewModel
{

    //Recordar, en los ViewModel es donde creamos/exponemos los objetos que serán linkados/bound con la vista

    public class LoginVM
    {
        //For the login functionality we will need --> An user, a register and a login button

        public UserModel User { get; set; }   

        public RegisterCommand RegisterCommand { get; set; }

        public LoginCommand LoginCommand { get; set; }

        //Evento que hemos creado nosotros:
        public event EventHandler HasLoggedIn; //Event that will be fired when the login or the register has been successfull

        public LoginVM()
        {
            User = new UserModel();
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
        }

        public async void Login()
        {
            //using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            //{
            //    conn.CreateTable<UserModel>();

            //    var user = conn.Table<UserModel>().Where(u => u.Username == User.Username).FirstOrDefault();

            //    if (user.Password == User.Password)
            //    {
            //        App.UserId = user.Id.ToString();
            //        HasLoggedIn(this, new EventArgs()); //FireEvent..este evento será recogido en LoginWindow.xaml.cs y cerrará la ventana de login,
            //        //dando paso a la NotesWindow
            //    }
            //}

            try
            {
                //El filtro (where) va a ser realizado por el servidor...Por eso el ToListAsync()... Nosotros no filtramos aquí..sería ineficiente
                var user = (await App.MobileServiceClient.GetTable<UserModel>().Where(u => u.Username == User.Username).ToListAsync()).FirstOrDefault();

                if (user.Password == User.Password)
                {
                    App.UserId = user.Id;
                    HasLoggedIn(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {

            }
        }

        //Este método es async porque dentro tiene un await...
        //Es decir, el tiene que esperar a que el getTable se complete
        //Quien ejecute el RegisterAsync() deberá hacerlo así --> await RegisterAsync()
        

        public async Task RegisterAsync()
        {
            //using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            //{
            //    conn.CreateTable<UserModel>();

            //    var result = DatabaseHelper.Insert(User);

            //    if (result)
            //    {
            //        App.UserId = User.Id.ToString();
            //        HasLoggedIn(this, new EventArgs());
            //    }
            //}

            //-----------------Funcionalidad con azure------------------------------------
            try
            {
                //Is an async method, so we have to await it..It means that we won´t logIn until the user is inserted
                //althought the app won´t be blocked of course
                await App.MobileServiceClient.GetTable<UserModel>().InsertAsync(User);
                App.UserId = User.Id.ToString();
                HasLoggedIn(this, new EventArgs());
            }
            catch (Exception ex)
            {

            }

        }


    }
}
