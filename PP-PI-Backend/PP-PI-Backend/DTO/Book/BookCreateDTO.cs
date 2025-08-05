namespace PP_PI_Backend.DTO.Book
{
    public class BookCreateDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime PublicationDate { get; set; } 

        public string ImageUrl { get; set; }
        public int AuthorId { get; set; }      // Must exist previously
        public int PublisherId { get; set; }   // Must exist previously

    }
}


