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
                    MessageBox.Show("✅ Conexión establecida con éxito");

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
            string correo = TxtCorreo.Text.Trim();
            string contrasena = TxtContrasena.Password; // en vez de TxtContrasena.Text

            if (ValidarUsuario(correo, contrasena))
            {
                MessageBox.Show("¡Inicio de sesión exitoso!", "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Information);
                // Aquí puedes abrir otra ventana o cambiar de vista
            }
            else
            {
                MessageBox.Show("Correo o contraseña incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
    }

}
