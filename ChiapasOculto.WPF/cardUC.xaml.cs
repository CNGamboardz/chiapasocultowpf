using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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
using static chiapasocultowpf.datos.ConexionDB;
using static chiapasocultowpf.datos.Modelo;

namespace ChiapasOculto.WPF
{
    /// <summary>
    /// Lógica de interacción para cardUC.xaml
    /// </summary>
    public partial class cardUC : UserControl
    {
        public cardUC()
        {
            InitializeComponent();
        }
        public void CargarDatos(string nombre, string correo, string telefono, string direccion)
        {
            NombreTextBox.Text = nombre;
            CorreoTextBox.Text = correo;
            TelefonoTextBox.Text = telefono;
            DireccionTextBox.Text = direccion;
        }

        public class OperadoraService
        {
            public List<Operadora> ObtenerTodasOperadoras()
            {
                List<Operadora> lista = new List<Operadora>();

                Conexion conexionDB = new Conexion();
                using (var conexion = conexionDB.AbrirConexion())
                {
                    string query = "SELECT * FROM operadoras ORDER BY id_operadoras ASC";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Operadora
                            {
                                id_operadoras = reader.GetInt32("id_operadoras"),
                                nombredeoperadoras = reader.GetString("nombredeoperadoras"),
                                correoelectronico = reader.GetString("correoelectronico"),
                                telefono = reader.GetString("telefono"),
                                direccion = reader.GetString("direccion")
                            });
                        }
                    }
                }

                return lista;
            }
        }
    }
}
