using System.ComponentModel.DataAnnotations;

namespace MusicApi.DTOs
{
    public class UpdateArtistDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = "";
        [StringLength(100)]
        public string Country { get; set; } = "";
    }
}
