namespace MusicApi.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int DurationSeconds { get; set; }
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }
        public List<Playlist> Playlists { get; set; } = new();

    }
}
