﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pagina1.Modelo
{
    public class Historial_Clinico
    {
        public int id_historial { get; set; }
        public int id_canino { get; set; }
        public DateTime fecha_historial { get; set; }
        public string tipo_historial { get; set; }
        public string descripcion_historial { get; set; }
        public bool notificacion_historial { get; set; }
        public Canino Canino { get; set; }

    }
}