using PP_PI_Backend.Models;

namespace PP_PI_Backend.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string WebsiteUrl { get; set; }
        public DateTime? FoundedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>(); // List of books that allows inverted navegation

    }
}
