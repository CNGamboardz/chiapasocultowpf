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
    /// Lógica de interacción para OperadoraDatos.xaml
    /// </summary>
    public partial class OperadoraDatos : Window
    {
        public OperadoraDatos()
        {
            InitializeComponent();
            Loaded += OperadoraDatos1_Loaded;
        }
        private void Inicio_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ventanaInicio = new MainWindow(); 
            ventanaInicio.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nombre = TxtOperadoraNombre.Text.Trim().ToUpper();
            string correo = TxtOperadoraCorreo.Text.Trim();
            string contrasena = TxtOperadoraContrasena.Password.Trim();
            string telefono = TxtOperadoraTelefono.Text.Trim();
            string direccion = TxtOperadoraDireccion.Text.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(TxtOperadoraNombre.Text) ||
                string.IsNullOrWhiteSpace(TxtOperadoraCorreo.Text) ||
                string.IsNullOrWhiteSpace(TxtOperadoraContrasena.Password) || // si es PasswordBox
                string.IsNullOrWhiteSpace(TxtOperadoraTelefono.Text) ||
                string.IsNullOrWhiteSpace(TxtOperadoraDireccion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de guardar.", "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UsuarioDAO dao = new UsuarioDAO();
            if (dao.RegistrarOperadora(nombre, correo, telefono, direccion, contrasena))
            {
                MessageBox.Show("Operadora registrada correctamente.");
            }
        }

        private void OperadoraDatos1_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Sesion.NombreCompleto))
            {
                Identificador.Text = Sesion.NombreCompleto;
            }

        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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
