using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animalitos
{
    [Activity(Label = "RegistroUsuarios")]
    public class RegistroUsuarios : Activity
    {
        EditText txtNuevoUsuario;
        EditText txtNuevaClaveUsuario;
        EditText txtNuevoNombreUsuario;
        EditText txtNuevoApellidoUsuario;
        EditText txtNuevoTelefonoUsuario;
        Button btnRegistrarUsuario;
        Button btnYaTienesCuenta;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegistroUsuarios);
            txtNuevoUsuario = FindViewById<EditText>(Resource.Id.txtNuevoUsuario);
            txtNuevaClaveUsuario = FindViewById<EditText>(Resource.Id.txtNuevaClaveUsuario);
            txtNuevoNombreUsuario = FindViewById<EditText>(Resource.Id.txtNuevoNombreUsuario);
            txtNuevoApellidoUsuario = FindViewById<EditText>(Resource.Id.txtNuevoApellidoUsuario);
            txtNuevoTelefonoUsuario = FindViewById<EditText>(Resource.Id.txtNuevoTelefonoUsuario);
            btnRegistrarUsuario = FindViewById<Button>(Resource.Id.btnRegistrarUsuarioNuevo);
            btnYaTienesCuenta = FindViewById<Button>(Resource.Id.btnYaTienesCuenta);

            btnRegistrarUsuario.Click += BtnRegistrarUsuario_Click;
            btnYaTienesCuenta.Click += BtnYaTienesCuenta_Click;

            Spinner rolspinner = FindViewById<Spinner>(Resource.Id.rolSpinner);

            rolspinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.rol, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            rolspinner.Adapter = adapter;
        }



        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("The city is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }





        private void BtnYaTienesCuenta_Click(object sender, System.EventArgs e)
        {
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }

        private void BtnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNuevoUsuario.Text.Trim()) && !string.IsNullOrEmpty(txtNuevaClaveUsuario.Text.Trim()) && (!string.IsNullOrEmpty(txtNuevoNombreUsuario.Text.Trim()) && (!string.IsNullOrEmpty(txtNuevoApellidoUsuario.Text.Trim()) && (!string.IsNullOrEmpty(txtNuevoTelefonoUsuario.Text.Trim())))))
                {
                    new Auxiliar().guardar(new Registro() { Id = 0, Usuario = txtNuevoUsuario.Text.Trim(), Password = txtNuevaClaveUsuario.Text.Trim(), Nombre = txtNuevoNombreUsuario.Text.Trim(), Apellido = txtNuevoNombreUsuario.Text.Trim(), Telefono = txtNuevoNombreUsuario.Text.Trim(), });
                    Toast.MakeText(this, "Registro guardado", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Por favor ingrese un nombre de usuario y una clave", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }



    }
}
