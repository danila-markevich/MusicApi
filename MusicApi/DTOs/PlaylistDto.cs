namespace MusicApi.DTOs
{
    public class PlaylistDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int SongCount { get; set; }
    }
}
