using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using chiapasocultowpf.logica;
using BCrypt.Net;

namespace ChiapasOculto.WPF
{
    /// <summary>
    /// Lógica de interacción para IniciarSesion.xaml
    /// </summary>
    public partial class IniciarSesion : Window
    {
        public IniciarSesion()
        {
            InitializeComponent();
        }


        public bool ValidarUsuario(string correo, string contrasena)
        {
            string connectionString = "Server=localhost;Database=chiapas_oculto;Uid=root;Pwd=;";
            string contrasenaHash = EncriptarSHA256(contrasena);
            bool existe = false;

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();

                    string query = @"
                SELECT COUNT(*) 
                FROM usuario 
                WHERE CorreoElectronico = @correo 
                AND (Contrasena = @contrasenaPlano OR Contrasena = @contrasenaHash)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@contrasenaPlano", contrasena);
                        cmd.Parameters.AddWithValue("@contrasenaHash", contrasenaHash);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        existe = count > 0;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al conectar: " + ex.Message);
            }

            return existe;
        }


        public static string EncriptarSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void BtnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            Registrarse ventanaRegistro = new Registrarse();
            ventanaRegistro.Show(); // Usa ShowDialog() si quieres que sea modal
            this.Close();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IniciarSesion ventanaIniciarSesion = new IniciarSesion(); // Cambia el nombre si tu otra ventana se llama diferente
            ventanaIniciarSesion.Show();
            this.Close(); // Cierra la ventana actual (opcional)
        }

        private void operadoras_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OperadoraDatos VentanaoperadoraDatos = new OperadoraDatos();
            VentanaoperadoraDatos.Show();
            this.Close(); // Opcional, si quieres cerrar la ventana actual
        }

        private void Registrase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Registrarse ventanaRegistro = new Registrarse();
            ventanaRegistro.Show(); // Usa ShowDialog() si quieres que sea modal
            this.Close();
        }

        private void btniniciarsesion_Click(object sender, RoutedEventArgs e)
        {
            string correo = TxtCorreo.Text.Trim();
            string contrasena = TxtContrasena.Password.Trim();

            string cadenaConexion = "server=localhost;port=3306;user=root;password=root;database=chiapas_oculto;";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    string consulta = "SELECT * FROM usuario WHERE CorreoElectronico = @correo";
                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@correo", correo);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string hashBD = reader.GetString("Contrasena");
                                string hashIngresado = HashPassword(contrasena); // Usamos tu método SHA256

                                if (hashIngresado == hashBD)
                                {
                                    // Guardar datos de sesión
                                    Sesion.IdUsuario = reader.GetInt32("id_usuario");
                                    Sesion.NombreCompleto = reader.GetString("NombreUsuario") + " " + reader.GetString("Apellido");
                                    Sesion.Rango = reader.GetInt32("id_rango");

                                    // Abrir ventana principal
                                    var ventanaInicio = new MainWindow(); // o Agregar_Eliminar
                                    ventanaInicio.Show();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Contraseña incorrecta", "Acceso denegado", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("El correo no está registrado", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar: " + ex.Message, "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }
    }

}
