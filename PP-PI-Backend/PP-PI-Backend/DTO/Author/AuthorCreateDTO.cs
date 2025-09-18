namespace PP_PI_Backend.DTO.Author
{
    public record AuthorCreateDTO
    {
        public string FirstName;
        public string LastName;
        public string? Nationality;
        public DateTime? Birthdate;
    }
}
