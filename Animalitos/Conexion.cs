using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Animalitos
{

    #region uso de datos registro de un usuario
    public class Registro
    {

        public Registro() { }

        [PrimaryKey, AutoIncrement]
        [MaxLength(10)]
        public int Id { get; set; }

        [MaxLength(15)]
        public string Nombre { get; set; }

        [MaxLength(15)]
        public string Apellido { get; set; }

        [MaxLength(15)]
        public string Telefono { get; set; }

        [MaxLength(15)]
        public string Usuario { get; set; }

        [MaxLength(15)]
        public string Password { get; set; }

        [NotNull]
        public string Rol { get; set; }

    }
    #endregion

    #region Manejo de datos y conexion a BD
    public class Auxiliar
    {
        static object loker = new object();
        SQLiteConnection connection;
        public Auxiliar()//Esto es un construtor
        {
            connection = conectarBD();
            connection.CreateTable<Registro>();
        }

        public SQLite.SQLiteConnection conectarBD()
        {
            SQLiteConnection connectionBaseDatos;
            string nombreArchivo = "registros.db3";
            string ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completaRuta = Path.Combine(ruta, nombreArchivo);
            connectionBaseDatos = new SQLiteConnection(completaRuta);
            return connectionBaseDatos;

        }

        //Selecionar un registro
        public Registro selecionarUno(string NuevoUsuario, string NuevaClaveUsuario)
        {
            lock (loker)
            {
                return connection.Table<Registro>().FirstOrDefault(x => x.Usuario == NuevoUsuario && x.Password == NuevaClaveUsuario);
            }
        }



        //Selecionar muchos
        public IEnumerable<Registro> selecionarTodo()
        {
            lock (loker)
            {
                return (from i in connection.Table<Registro>() select i).ToList();
            }
        }

        //Guardar o actualizar
        public int guardar(Registro registro)
        {
            lock (loker)
            {
                if (registro.Id == 0)
                {
                    return connection.Insert(registro);
                }
                else
                {
                    return connection.Update(registro);
                }
            }
        }

        //Eliminar
        public int eliminar(int ID)
        {
            lock (loker)
            {
                return connection.Delete<Registro>(ID);
            }
        }

    }
    #endregion
}