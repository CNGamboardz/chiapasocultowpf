using chiapasocultowpf.logica;
using Microsoft.Win32;
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
using static ChiapasOculto.WPF.cardUC;
using static chiapasocultowpf.datos.Modelo;


namespace ChiapasOculto.WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Grid_Loaded;

        }

        private void Registrase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (!Sesion.EstaLogueado)
            {
                MessageBox.Show("Debes iniciar sesión para usar esta función.", "Acceso Denegado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            OperadoraDatos ventanaOperadoraDatos = new OperadoraDatos();
            ventanaOperadoraDatos.Show();
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Sesion.EstaLogueado)
            {
                MessageBox.Show("Debes iniciar sesión para usar esta función.", "Acceso Denegado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Modificar_Eliminar ventanaEliminar = new Modificar_Eliminar();
            ventanaEliminar.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!Sesion.EstaLogueado)
            {
                MessageBox.Show("Debes iniciar sesión para usar esta función.", "Acceso Denegado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Modificar_Eliminar ventanaModificar = new Modificar_Eliminar();
            ventanaModificar.Show();
            this.Close();
        }


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Sesion.NombreCompleto))
            {
                Identificador.Text = Sesion.NombreCompleto;
            }

            OperadoraService servicio = new OperadoraService();
            List<Operadora> lista = servicio.ObtenerTodasOperadoras(); // Ya debes tener este método implementado
            TarjetasOperadorasPanel.Children.Clear();

            foreach (var datos in lista)
            {
                // Crear instancia de la tarjeta
                var tarjeta = new cardUC();
                tarjeta.CargarDatos(datos.nombredeoperadoras, datos.correoelectronico, datos.telefono, datos.direccion);

                // Agregar al panel
                TarjetasOperadorasPanel.Children.Add(tarjeta);
            }
        }

        private void Inciarsesionbtn_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Sesion.NombreCompleto))
            {
                Inciarsesionbtn.IsEnabled = false;
                Inciarsesionbtn.Opacity = 0.5;
            }
        }

        private void cerrarsesion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

