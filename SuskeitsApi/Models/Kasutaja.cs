namespace SuskeitsApi.Models
{
    public class Kasutaja
    {

        public int Id { get; set; }
        public string Kasutajanimi { get; set; }
        public string Parool { get; set; }
        public string Eesnimi { get; set; }
        public string Perenimi { get; set; }

        // One user can have many products
        public ICollection<Toode> Tooted { get; set; }

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
