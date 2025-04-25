using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace chiapasocultowpf.datos
{
    public class Modelo
    {
        public class Operadora
        {
            public int id_operadoras { get; set; }
            public string nombredeoperadoras { get; set; }
            public string correoelectronico { get; set; }
            public string contrasena { get; set; }
            public string telefono { get; set; }
            public string direccion { get; set; }
        }
    }
}

