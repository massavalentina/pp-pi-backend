namespace PP_PI_Backend.DTO
{
    public class AuthorCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Nationality { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
