namespace Pagina1.Modelo
{
    public class Canino
    {
        public int id_canino { get; set; }
        public string nombre_canino { get; set; }
        public int edad_canino { get; set; }
        public string raza_canino { get; set; }
        public decimal peso_canino { get; set; }
        public byte[] foto_canino { get; set; }
        public string cedula_dueno { get; set; }

        public Dueno Dueno { get; set; }

    }

}
