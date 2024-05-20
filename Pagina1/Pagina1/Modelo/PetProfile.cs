using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pagina1.Modelo
{
    public class PetProfile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public int Edad { get; set; }
        public float Peso { get; set; }
        public byte[] FotoBytes { get; set; }
        public string RecetasGuardadasSerialized { get; set; }
        public string VeterinarioSerialized { get; set; }
        public string AlergiasSerialized { get; set; }
        public string EnfermedadesConocidasSerialized { get; set; }
        public string HistorialMedico { get; set; }

        [Ignore]
        public List<string> RecetasGuardadas
        {
            get => DeserializeList(RecetasGuardadasSerialized);
            set => RecetasGuardadasSerialized = SerializeList(value);
        }

        [Ignore]
        public List<string> Veterinario
        {
            get => DeserializeList(VeterinarioSerialized);
            set => VeterinarioSerialized = SerializeList(value);
        }

        [Ignore]
        public List<string> Alergias
        {
            get => DeserializeList(AlergiasSerialized);
            set => AlergiasSerialized = SerializeList(value);
        }

        [Ignore]
        public List<string> EnfermedadesConocidas
        {
            get => DeserializeList(EnfermedadesConocidasSerialized);
            set => EnfermedadesConocidasSerialized = SerializeList(value);
        }

        private string SerializeList(List<string> list)
        {
            if (list == null || list.Count == 0)
                return null;

            return string.Join(",", list.Select(s => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(s))));
        }

        private List<string> DeserializeList(string serialized)
        {
            if (string.IsNullOrEmpty(serialized))
                return new List<string>();

            return serialized.Split(',').Select(s => System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(s))).ToList();
        }
    }
}