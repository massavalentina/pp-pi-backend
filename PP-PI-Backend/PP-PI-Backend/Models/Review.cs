using System.ComponentModel.DataAnnotations;

namespace PP_PI_Backend.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
