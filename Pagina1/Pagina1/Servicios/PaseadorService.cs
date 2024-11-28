﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pagina1.Servicios
{
    public class PaseadorService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "http://10.0.2.2:5138/api/";

        public static async Task<List<CaninoConDuenoDTO>> ObtenerCaninosConDuenosAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{BaseUrl}/api/Paseadores/CaninosConDuenos");
                return JsonConvert.DeserializeObject<List<CaninoConDuenoDTO>>(response);
            }
        }
    }

    public class CaninoConDuenoDTO
    {
        public int IdCanino { get; set; }
        public string NombreCanino { get; set; }
        public string Raza { get; set; }
        public int EdadCanino { get; set; }
        public decimal PesoCanino { get; set; }
        public string NombreDueno { get; set; }
        public string DireccionDueno { get; set; }
        public string CelularDueno { get; set; }
    }
}