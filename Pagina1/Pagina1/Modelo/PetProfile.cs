using SQLite;
using System.Collections.Generic;

namespace Pagina1.Modelo
{
    public class PetProfile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public byte[] FotoBytes { get; set; } 
        public List<string> RecetasGuardadas { get; set; }
        public List<string> Veterinario { get; set; }
        public List<string> Alergias { get; set; }
        public List<string> EnfermedadesConocidas { get; set; }
        public string HistorialMedico { get; set; }
    }
}