namespace MusicApi.DTOs
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int DurationSeconds { get; set; }
        public int ArtistId { get; set; }
        public string? ArtistName { get; set; }
    }
}
