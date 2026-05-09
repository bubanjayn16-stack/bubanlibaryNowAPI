using System.ComponentModel.DataAnnotations;

namespace bubanlibaryNowAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public bool Available { get; set; }

        public int PublishedYear { get; set; }
    }
}
