namespace PP_PI_Backend.DTO.Review
{
    public class ReviewCreateDTO
    {
        public int BookId { get; set; } // associated book
        public string Comment { get; set; }
        public int Rating { get; set; }
    }

}
