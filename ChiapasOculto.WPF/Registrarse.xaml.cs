using chiapasocultowpf.datos;
using chiapasocultowpf.logica;
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
            Loaded += Registrarse1_Loaded;
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
            if (!string.IsNullOrEmpty(Sesion.NombreCompleto))
            {
                MessageBox.Show("Ya has iniciado sesión.", "Acceso denegado", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
        private void Label_MouseDoubleClick1(object sender, MouseButtonEventArgs e)    
        {
            MainWindow ventanaInicio = new MainWindow(); // Cambia el nombre si tu otra ventana se llama diferente
            ventanaInicio.Show();
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
            string nombre = TxtNombre.Text.Trim().ToUpper();
            string apellido = TxtApellidos.Text.Trim().ToUpper();  // Reemplaza con el TextBox correspondiente
            string correo = TxtCorreo.Text.Trim();       // Reemplaza con el TextBox correspondiente
            string telefono = TxtTelefono.Text.Trim();    // Reemplaza con el TextBox correspondiente
            string contrasena = TxtContrasena.Password.Trim(); // Reemplaza con el PasswordBox correspondiente

            if (string.IsNullOrWhiteSpace(TxtNombre.Text) ||
                string.IsNullOrWhiteSpace(TxtCorreo.Text) ||
                string.IsNullOrWhiteSpace(TxtTelefono.Text) ||
                string.IsNullOrWhiteSpace(TxtApellidos.Text) ||
                string.IsNullOrWhiteSpace(TxtContrasena.Password) ||
                string.IsNullOrWhiteSpace(TxtNombre.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UsuarioDAO dao = new UsuarioDAO();
            if (dao.RegistrarAdministrador(nombre, apellido, correo, telefono, contrasena))
            {
                MessageBox.Show("Administrador registrado correctamente.");
                LimpiarCampos();

                IniciarSesion ventanaIniciarSesion = new IniciarSesion(); // Cambia el nombre si tu otra ventana se llama diferente
                ventanaIniciarSesion.Show();
                this.Close(); // Cierra la ventana actual (opcional)
            }
    }
        private void LimpiarCampos()
        {
            TxtNombre.Text = "";
            TxtApellidos.Text = "";
            TxtCorreo.Text = "";
            TxtTelefono.Text = "";
            TxtContrasena.Password = "";
        }

        private void Registrarse1_Loaded(object sender, RoutedEventArgs e)
        {
         

            if (!string.IsNullOrEmpty(Sesion.NombreCompleto))
            {
                Identificador.Text = Sesion.NombreCompleto;
            }

            
        }
        
        private void Iniciarsesion_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Sesion.NombreCompleto))
            {
                Iniciarsesion.IsEnabled = false;
                Iniciarsesion.Opacity = 0.5;
            }
        }
        private void Label_MouseDoubleClick2(object sender, MouseButtonEventArgs e)
        {
            // Validar que haya una sesión activa
            if (string.IsNullOrEmpty(Sesion.NombreCompleto))
            {
                MessageBox.Show("No hay ninguna sesión iniciada.", "Cerrar sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Confirmar cierre de sesión
            var resultado = MessageBox.Show("¿Estás seguro de que deseas cerrar sesión?", "Cerrar sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                // Limpiar sesión
                Sesion.CerrarSesion();

                // Volver al login
                IniciarSesion login = new IniciarSesion();
                login.Show();

                // Cerrar esta ventana
                this.Close();
            }
        }

    }
}
