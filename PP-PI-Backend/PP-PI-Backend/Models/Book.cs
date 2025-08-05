using PP_PI_Backend.Entities;

namespace PP_PI_Backend.Models
{
    public class Book
    {
        public int Id { get; set; } // Book ID
        public string Title { get; set; } // Book title

        public string Description { get; set; } // Book description
        public DateTime PublicationDate { get; set; } // Date when the book was published
        public string ImageUrl { get; set; } // URL for the books image
        public int AuthorId {  get; set; } // Author ID
        public Author Author {get; set; } // Author object

        public int PublisherId { get; set; } // Publisher ID
        public Publisher Publisher {get; set;} // Publisher object
        public ICollection<Review> Reviews {get; set;} = new List<Review>(); 
        
    }
}
