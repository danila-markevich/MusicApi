
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MusicApi.Data;
using MusicApi.DTOs;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlaylistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlaylistDto>>> GetAll()
        {
            var playlists = await _context.Playlists
                .Include(p => p.Songs)
                .Select(p => new PlaylistDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    SongCount = p.Songs.Count()
                })
                .ToListAsync();
            return playlists;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistDto>> GetById (int id)
        {
            var playlist = await _context.Playlists
                .Include (p => p.Songs)
                .Select(p=> new PlaylistDto
                {
                    Id= p.Id,
                    Name= p.Name,
                    Description= p.Description,
                    SongCount= p.Songs.Count()
                })
                .FirstOrDefaultAsync(p=> p.Id == id);
            if (playlist == null) return NotFound();
            return playlist;
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistDto>> Create (CreatePlaylistDto dto)
        {
            var playlist = new Playlist
            {
                Name = dto.Name,
                Description = dto.Description,

            };

            _context.Playlists.Add(playlist);

            await _context.SaveChangesAsync();

            var result = new PlaylistDto
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description,
                SongCount = 0
            };

            return CreatedAtAction(nameof(GetById), new { id = playlist.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update (int id, UpdatePlaylistDto dto)
        {
            var existing = await _context.Playlists.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Name = dto.Name;
            existing.Description = dto.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null) return NotFound();

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{playlistId}/songs/{songId}")]
        public async Task<IActionResult> AddSong(int playlistId, int songId)
        {
            var playlist = await _context.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(p => p.Id == playlistId);

            if (playlist == null) return NotFound("Playlist not found");

            var song = await _context.Songs.FindAsync(songId);
            if (song == null) return NotFound("Song not found");

            if (playlist.Songs.Contains(song))
            {
                return BadRequest("Song already in playlist");
            }

            playlist.Songs.Add(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{playlistId}/songs/{songId}")]
        public async Task<IActionResult> RemoveSong(int playlistId, int songId)
        {
            var playlist = await _context.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(p => p.Id == playlistId);

            if (playlist == null) return NotFound("Playlist not found");

            var song = playlist.Songs.FirstOrDefault(s => s.Id == songId);
            if (song == null) return NotFound("Song not in playlist");

            playlist.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{playlistId}/songs")]
        public async Task<ActionResult<List<SongDto>>> GetSongs(int playlistId)
        {
            var playlistExists = await _context.Playlists.AnyAsync(p => p.Id == playlistId);
            if (!playlistExists) return NotFound("Playlist not found");

            var songs = await _context.Playlists
                .Where(p => p.Id == playlistId)
                .SelectMany(p => p.Songs)
                .Select(s => new SongDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    DurationSeconds = s.DurationSeconds,
                    ArtistId = s.ArtistId,
                    ArtistName = s.Artist != null ? s.Artist.Name : null
                })
                .ToListAsync();

            return songs;
        }
    }
}
