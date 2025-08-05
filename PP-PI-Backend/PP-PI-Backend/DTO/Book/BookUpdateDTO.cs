namespace PP_PI_Backend.DTO.Book
{
    public class BookUpdateDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }

        public string ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
    }
}
