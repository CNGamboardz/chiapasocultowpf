using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using static chiapasocultowpf.datos.ConexionDB;
using static chiapasocultowpf.datos.Modelo;

namespace Datos
{
    public class OperadoraDatos : Conexion
    {
        public List<Operadora> ObtenerTodas()
        {
            List<Operadora> lista = new List<Operadora>();

            string query = "SELECT * FROM operadoras";

            try
            {
                using (MySqlConnection conn = AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Operadora
                            {
                                nombredeoperadoras = reader.GetString("nombredeoperadoras"),
                                correoelectronico = reader.GetString("correoelectronico"),
                                contrasena = reader.GetString("contraseña"),
                                telefono = reader.GetString("telefono"),
                                direccion = reader.GetString("direccion")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener operadoras: " + ex.Message);
            }

            return lista;
        }
    }
}
