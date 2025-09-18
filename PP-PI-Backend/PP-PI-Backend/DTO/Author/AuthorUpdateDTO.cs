namespace PP_PI_Backend.DTO.Author
{
    public record AuthorUpdateDTO
    {
        int Id;
        string FirstName;
        string LastName;
        string? Nationality;
        DateTime? Birthdate;
    }
}
