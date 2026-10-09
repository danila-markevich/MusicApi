using System.ComponentModel.DataAnnotations;

namespace MusicApi.DTOs
{
    public class CreateSongDto
    {
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Title { get; set; } = "";
        [Range(1, 3600)]
        public int DurationSeconds { get; set; }
        [Range(1, int.MaxValue)]
        public int ArtistId { get; set; }
    }
}
