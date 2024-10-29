namespace SuskeitsApi.Models
{
    public class Toode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

        // Many-to-many relationship with Kasutaja through KasutajaToode
        public List<KasutajaToode> KasutajaTooted { get; set; } = new List<KasutajaToode>();

        // Parameterless constructor for EF Core
        public Toode() { }

        // Constructor with parameters
        public Toode(int id, string name, double price, bool isActive)
        {
            Id = id;
            Name = name;
            Price = price;
            IsActive = isActive;
        }
    }
}
