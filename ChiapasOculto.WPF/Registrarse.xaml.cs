using chiapasocultowpf.datos;
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
using System.Windows.Shapes;

namespace ChiapasOculto.WPF
{
    /// <summary>
    /// Lógica de interacción para Registrarse.xaml
    /// </summary>
    public partial class Registrarse : Window
    {
        public Registrarse()
        {
            InitializeComponent();
        }
        private void BtnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion ventanaIniciarSesion = new IniciarSesion(); // Cambia el nombre si tu otra ventana se llama diferente
            ventanaIniciarSesion.Show();
            this.Close(); // Cierra la ventana actual (opcional)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nombre = TxtNombre.Text.Trim();
            string apellido = "Apellidos";  // Reemplaza con el TextBox correspondiente
            string correo = "Correo";       // Reemplaza con el TextBox correspondiente
            string telefono = "Celular";    // Reemplaza con el TextBox correspondiente
            string contrasena = "Password"; // Reemplaza con el PasswordBox correspondiente

            UsuarioDAO dao = new UsuarioDAO();
            if (dao.RegistrarAdministrador(nombre, apellido, correo, telefono, contrasena))
            {
                MessageBox.Show("Administrador registrado correctamente.");
            }
    }
}
}
