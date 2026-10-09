using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.DTOs;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArtistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArtistDto>>> GetAll()
        {
            var artists = await _context.Artists
                .Select(a => new ArtistDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Country = a.Country,
                    SongsCount = a.Songs.Count()
                })
                .ToListAsync();

            return artists;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDto>> GetById(int id)
        {
            var artist = await _context.Artists
                .Select(a => new ArtistDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Country = a.Country,
                    SongsCount = a.Songs.Count()
                })
                .FirstOrDefaultAsync(a => a.Id == id);
            if (artist == null) return NotFound();
            return artist;
        }

        [HttpPost]
        public async Task<ActionResult<ArtistDto>> Create(CreateArtistDto dto)
        {
            var artist = new Artist
            {
                Name = dto.Name,
                Country = dto.Country
            };

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            var result = new ArtistDto
            {
                Id = artist.Id,
                Name = artist.Name,
                Country = artist.Country,
                SongsCount = 0
            };

            return CreatedAtAction(nameof(GetById), new { id = artist.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateArtistDto dto)
        {
            var existing = await _context.Artists.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Name = dto.Name;
            existing.Country = dto.Country;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return NotFound();
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}