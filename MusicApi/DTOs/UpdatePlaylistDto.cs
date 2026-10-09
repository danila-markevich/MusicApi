using System.ComponentModel.DataAnnotations;


namespace MusicApi.DTOs
{
    public class UpdatePlaylistDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = "";
        [StringLength(300)]
        public string Description { get; set; } = "";
    }
}
