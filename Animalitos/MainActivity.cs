using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using SQLite;
using System;
using System.ComponentModel;
using System.Linq;

namespace Animalitos
{

    [Activity(Label = "@string/app_name", Icon = "@mipmap/ic_IconoV", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txtUser;
        EditText txtPassw;
        Button btnIngresar;
        Button btnRegistrate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            txtUser = FindViewById<EditText>(Resource.Id.txtUser);
            txtPassw = FindViewById<EditText>(Resource.Id.txtPassw);
            btnIngresar = FindViewById<Button>(Resource.Id.btnIngresar);
            btnRegistrate = FindViewById<Button>(Resource.Id.btnRegistrate);

            btnIngresar.Click += BtnIngresar_Click;
            btnRegistrate.Click += BtnRegistrate_Click;

        }

        private void BtnRegistrate_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(RegistroUsuarios));
            StartActivity(i);
        }


        private void BtnIngresar_Click(object sender, System.EventArgs e)
        {
            string username = FindViewById<EditText>(Resource.Id.txtUser).Text;
            string password = FindViewById<EditText>(Resource.Id.txtPassw).Text;

            if (AuthenticateUser(username, password))
            {
                string role = GetUserRole(username);
                if (role == "administrador")
                {
                    // hacer algo solo para usuarios con rol de administrador
                }
                else if (role == "medico_veterinario")
                {
                    // hacer algo solo para usuarios con rol de médico veterinario
                }
                else if (role == "dueño_de_mascota")
                {
                    // hacer algo solo para usuarios con rol de dueño de mascota
                }
                else if (role == "vendedor")
                {
                    // hacer algo solo para usuarios con rol de vendedor
                }
            }
            else
            {
                // mostrar mensaje de error
            }
        }


        private bool AuthenticateUser(string username, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                connection.CreateTable<Registro>();
                return connection.Table<Registro>().Any(u => u.Usuario == username && u.Password == password);
            }
        }

        private string GetUserRole(string username)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                connection.CreateTable<Registro>();
                return connection.Table<Registro>().FirstOrDefault(u => u.Usuario == username)?.Rol;
            }
        }

        private readonly string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myapp.db3");



        private void Close()
        {
            throw new NotImplementedException();
        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}