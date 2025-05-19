using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using chiapasocultowpf.logica;
using MySql.Data.MySqlClient;
using static chiapasocultowpf.datos.Modelo;


namespace ChiapasOculto.WPF
{
    /// <summary>
    /// Lógica de interacción para Modificar_Eliminar.xaml
    /// </summary>
    /// 

    public partial class Modificar_Eliminar : Window
    {

        private ObservableCollection<Operadora> listaOperadoras;

        public Modificar_Eliminar()
        {
            InitializeComponent();
            Loaded += Agregar_Eliminar_Loaded;

        }
        

        private void Agregar_Eliminar_Loaded(object sender, RoutedEventArgs e)
        {
            CargarOperadoras();

            if (!string.IsNullOrEmpty(Sesion.NombreCompleto))
            {
                Identificador.Text = Sesion.NombreCompleto;
            }

            CargarOperadoras();

        }
        private void CargarOperadoras()
        {
            string cadenaConexion = "server=localhost;port=3306;user=root;password=;database=chiapas_oculto;";
            listaOperadoras = new ObservableCollection<Operadora>();

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string consulta = "SELECT id_operadoras, nombredeoperadoras, correoelectronico, telefono, direccion FROM operadoras";
                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaOperadoras.Add(new Operadora
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

                datosOperadora.ItemsSource = listaOperadoras;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message);
            }
        }

        private void datosOperadora_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datosOperadora.SelectedItem is Operadora seleccionada)
            {
                txtNombre.Text = seleccionada.nombredeoperadoras;
                txtCorreo.Text = seleccionada.correoelectronico;
                txtContrasena.Text = ""; // no mostrar hash
                txtTelefono.Text = seleccionada.telefono;
                txtDireccion.Text = seleccionada.direccion;

                // Deshabilitar campos
                HabilitarCampos(false);
            }
        }

        private string ObtenerNombreAdministrador(int idUsuario)
        {
            string nombreCompleto = string.Empty;
            string cadenaConexion = "server=localhost;port=3306;user=root;password=;database=chiapas_oculto;";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();
                string consulta = "SELECT NombreUsuario, Apellido FROM usuario WHERE id_rango = 2 AND id_usuario = @id";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nombreCompleto = reader.GetString("NombreUsuario") + " " + reader.GetString("Apellido");
                        }
                    }
                }
            }

            return nombreCompleto;
        }

        private void HabilitarCampos(bool estado)
        {
            txtNombre.IsEnabled = estado;
            txtCorreo.IsEnabled = estado;
            txtContrasena.IsEnabled = estado;
            txtTelefono.IsEnabled = estado;
            txtDireccion.IsEnabled = estado;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCampos(true);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (datosOperadora.SelectedItem is Operadora seleccionada)
            {
                string cadenaConexion = "server=localhost;port=3306;user=root;password=;database=chiapas_oculto;";

                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {

                    conexion.Open();
                    string consulta = @"UPDATE operadoras 
                                SET nombredeoperadoras = @nombre, 
                                    correoelectronico = @correo, 
                                    telefono = @telefono, 
                                    direccion = @direccion 
                                WHERE id_operadoras = @id";

                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                        cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                        cmd.Parameters.AddWithValue("@id", seleccionada.id_operadoras);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            var resultado = MessageBox.Show("¿Estás seguro de que deseas guardar los cambios?", "Confirmar modificación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                            if (resultado != MessageBoxResult.Yes)
                                return;

                            MessageBox.Show("Operadora modificada correctamente.");
                            CargarOperadoras(); // refrescar la tabla
                            HabilitarCampos(false); // volver a bloquear
                        }
                        else
                        {
                            MessageBox.Show("No se pudo modificar la operadora.");
                        }
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (datosOperadora.SelectedItem is Operadora seleccionada)
            {
                var resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar esta operadora?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultado != MessageBoxResult.Yes)
                    return;

                string cadenaConexion = "server=localhost;port=3306;user=root;password=;database=chiapas_oculto;";

                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    string consulta = "DELETE FROM operadoras WHERE id_operadoras = @id";
                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", seleccionada.id_operadoras);
                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("Operadora eliminada correctamente.");
                            CargarOperadoras();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar la operadora.");
                        }
                    }
                }
            }

        }
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtContrasena.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Validar que haya una sesión activa
            if (string.IsNullOrEmpty(Sesion.NombreCompleto))
            {
                MessageBox.Show("No hay ninguna sesión iniciada.", "Cerrar sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } 

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

        private void Inicio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var ventanaInicio = new MainWindow();
            ventanaInicio.Show();
            this.Close();
        }
    }
}
