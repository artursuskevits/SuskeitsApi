namespace SuskeitsApi.Models
{
    public class Kasutaja
    {
        public int Id { get; set; }
        public string Kasutajanimi { get; set; }
        public string Parool { get; set; }
        public string Eesnimi { get; set; }
        public string Perenimi { get; set; }

        // Many-to-many relationship with Toode through KasutajaToode
        public List<KasutajaToode> KasutajaTooted { get; set; } = new List<KasutajaToode>();

        // Parameterless constructor for EF Core
        public Kasutaja() { }

        // Constructor with parameters
        public Kasutaja(int id, string kasutajanimi, string parool, string eesnimi, string perenimi)
        {
            Id = id;
            Kasutajanimi = kasutajanimi;
            Parool = parool;
            Eesnimi = eesnimi;
            Perenimi = perenimi;
        }
    }
}
