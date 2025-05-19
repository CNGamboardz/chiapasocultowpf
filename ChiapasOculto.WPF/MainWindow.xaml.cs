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
            for (int i = 0; i < 20; i++)
            {
                cardUC card = new cardUC();
                contenedorWrapPanel.Children.Add(card);
            }
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
            OperadoraDatos VentanaoperadoraDatos = new OperadoraDatos();
            VentanaoperadoraDatos.Show();
            this.Close(); // Opcional, si quieres cerrar la ventana actual
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Modificar_Eliminar VentanaModificar_Eliminar = new Modificar_Eliminar();
            VentanaModificar_Eliminar.Show();
            this.Close(); // Opcional, si quieres cerrar la ventana actual
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Modificar_Eliminar VentanaModificar_Eliminar = new Modificar_Eliminar();
            VentanaModificar_Eliminar.Show();
            this.Close(); // Opcional, si quieres cerrar la ventana actual
        }
    }
}
