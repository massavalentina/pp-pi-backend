namespace PP_PI_Backend.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? Nationality { get; set; }

        public DateTime? Birthdate { get; set; }
        public ICollection<Book> Books { get; set;} = new List<Book>(); // List of books that allows inverted navegation
    }
}
