namespace SuskeitsApi.Models
{
    public class KasutajaToode
    {
        public int KasutajaId { get; set; }
        public Kasutaja Kasutaja { get; set; }

        public int ToodeId { get; set; }
        public Toode Toode { get; set; }
    }
}
