namespace SuskeitsApi.Models
{
    public class Toode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

        // Foreign key to link to the user
        public int KasutajaId { get; set; }
        public Kasutaja Kasutaja { get; set; }
        // Parameterless constructor for EF Core
        public Toode() { }

        // Custom constructor
        public Toode(int id, string name, double price, bool isActive)
        {
            Id = id;
            Name = name;
            Price = price;
            IsActive = isActive;
        }
    }
}
