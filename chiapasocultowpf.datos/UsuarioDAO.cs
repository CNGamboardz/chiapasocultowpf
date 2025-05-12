using System;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Forms;

namespace chiapasocultowpf.datos
{
    public class UsuarioDAO : ConexionDB.Conexion
    {
        public bool RegistrarAdministrador(string nombre, string apellido, string correo, string telefono, string contrasena)
        {
            try
            {
                string contrasenaHash = HashPassword(contrasena);

                string query = @"INSERT INTO usuario (NombreUsuario, Apellido, CorreoElectronico, NumeroTelefono, Contrasena, id_rango)
                                 VALUES (@nombre, @apellido, @correo, @telefono, @contrasena, 1)";

                using (MySqlCommand cmd = new MySqlCommand(query, AbrirConexion()))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido", apellido);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@contrasena", contrasenaHash);

                    cmd.ExecuteNonQuery();
                }

                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}");
                return false;
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

        public (string Nombre, string Apellido)? ValidarInicioSesion(string correo, string contrasena)
        {
            try
            {
                string contrasenaHash = HashPassword(contrasena);

                string query = "SELECT NombreUsuario, Apellido FROM usuario WHERE CorreoElectronico = @correo AND Contrasena = @contrasena";

                using (MySqlCommand cmd = new MySqlCommand(query, AbrirConexion()))
                {
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@contrasena", contrasenaHash);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nombre = reader["NombreUsuario"].ToString();
                            string apellido = reader["Apellido"].ToString();
                            return (nombre, apellido);
                        }
                    }
                }

                CerrarConexion();
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}");
                return null;
            }
        }

        public bool RegistrarOperadora(string nombre, string correo, string telefono, string direccion, string contrasena)
        {
            try
            {
                string contrasenaHash = HashPassword(contrasena);

                string query = @"INSERT INTO operadoras (nombredeoperadoras, correoelectronico, contrasena, telefono, direccion, id_rango)
                 VALUES (@nombre, @correo, @contrasena, @telefono, @direccion, 3)";


                using (MySqlCommand cmd = new MySqlCommand(query, AbrirConexion()))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@contrasena", contrasenaHash);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@direccion", direccion);

                    cmd.ExecuteNonQuery();
                }

                CerrarConexion();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar operadora: {ex.Message}");
                return false;
            }
        }

    }
}
