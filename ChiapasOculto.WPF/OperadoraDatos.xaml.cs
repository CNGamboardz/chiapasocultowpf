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
        }
        private void Inicio_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ventanaInicio = new MainWindow(); 
            ventanaInicio.Show();
            this.Close();
        }
    }
}
