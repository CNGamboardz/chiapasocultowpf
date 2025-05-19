using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chiapasocultowpf.logica
{
    public class Sesion
    {
        public static int IdUsuario { get; set; }
        public static string NombreCompleto { get; set; }
        public static int Rango { get; set; }
        public static bool EstaLogueado => IdUsuario > 0;

        public static void CerrarSesion()
        {
            IdUsuario = 0;
            NombreCompleto = null;
            Rango = 0;
        }
    }
}
