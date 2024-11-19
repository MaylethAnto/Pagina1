using System;
using System.Collections.Generic;
using System.Text;

namespace Pagina1.Modelo
{
    public class Ejercicio
    {
        public int id_ejercicio { get; set; }
        public int id_canino { get; set; }
        public DateTime? fecha_ejercicio { get; set; }
        public int duracion { get; set; }
        public string tipo_ejercicio { get; set; }
        public string nombre_truco { get; set; }
        public byte[] foto_ejercicio { get; set; }
        public Canino Canino { get; set; }
    }
}
