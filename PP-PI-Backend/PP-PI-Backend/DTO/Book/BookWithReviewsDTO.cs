using PP_PI_Backend.DTO.Review;

namespace PP_PI_Backend.DTO.Book
{
    public class BookWithReviewsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ImageUrl { get; set; }

        public string AuthorName { get; set; }
        public string PublisherName { get; set; }

        public List<ReviewDTO> Reviews { get; set; } = new();

    }
}
