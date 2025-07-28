namespace PP_PI_Backend.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string WebsiteUrl { get; set; }
        public DateTime? FoundedAt { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
