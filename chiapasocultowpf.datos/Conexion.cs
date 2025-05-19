using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;
using MySql.Data;

namespace chiapasocultowpf.datos
{
    public class ConexionDB
    {
        public class Conexion
        {
            private readonly string cadenaConexion = 
                "server=localhost;port=3306;user=root;password=;database=chiapas_oculto;";
            protected MySqlConnection conexion;


            public MySqlConnection AbrirConexion()
            {
                conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();
                return conexion;
            }

            public void CerrarConexion()
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                    conexion.Close();
            }
        }
    }
}

