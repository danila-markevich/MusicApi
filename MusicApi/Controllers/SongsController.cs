using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.DTOs;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SongDto>>> GetAll()
        {
            var song = await _context.Songs
                .Include(s => s.Artist)
                .Select(s => new SongDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    DurationSeconds = s.DurationSeconds,
                    ArtistId = s.ArtistId,
                    ArtistName = s.Artist != null ? s.Artist.Name : null,
                })
                .ToListAsync();
            return song;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SongDto>> GetById (int id)
        {
            var song = await _context.Songs
                .Include (s => s.Artist)
                .Select(s => new SongDto
                {
                    Id= s.Id,
                    Title= s.Title,
                    DurationSeconds= s.DurationSeconds,
                    ArtistId= s.ArtistId,
                    ArtistName = s.Artist != null ? s.Artist.Name : null,
                })
                .FirstOrDefaultAsync(s=> s.Id == id);
            if (song == null) return NotFound();
            return song;
        }

        [HttpPost]
        public async Task<ActionResult<SongDto>> Create (CreateSongDto dto)
        {
            var song = new Song
            {
                Title = dto.Title,
                DurationSeconds = dto.DurationSeconds,
                ArtistId = dto.ArtistId,
            };

            _context.Songs .Add (song);
            await _context.SaveChangesAsync();

            var artist = await _context.Artists.FindAsync(dto.ArtistId);

            var result = new SongDto
            {
                Id = song.Id,
                Title = song.Title,
                DurationSeconds = song.DurationSeconds,
                ArtistId = song.ArtistId,
                ArtistName = artist != null ? artist.Name : null,
            };

            return CreatedAtAction(nameof(GetById), new { id = song.Id }, result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSongDto dto)
        {
            var existing = await _context.Songs.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Title = dto.Title;
            existing.DurationSeconds = dto.DurationSeconds;
            existing.ArtistId = dto.ArtistId;
            
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null) return NotFound();
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
