namespace MusicApi.DTOs
{
    public class ArtistDto
    {
        public int Id { get; set; }        
        public string Name { get; set; } = "";
        public string Country { get; set; } = "";
        public int SongsCount { get; set; }
    }
}
