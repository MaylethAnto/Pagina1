using System;
using System.Collections.Generic;
using System.Text;

namespace Pagina1.Modelo
{
    public class Paseador
    {
        public int id_paseador { get; set; }
        public string nombre_paseador { get; set; }
        public string apellido_paseador { get; set; }
        public string direccion_paseador { get; set; }
        public string celular_paseador { get; set; }
        public string correo_paseador { get; set; }
        public int id_canino { get; set; }
        public Canino Canino { get; set; }
    }
}
