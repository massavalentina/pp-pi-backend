namespace PP_PI_Backend.DTO.Book
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }

        public string ImageUrl { get; set; }


        // Additional information to show in front, not id’s
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }

    }
}
