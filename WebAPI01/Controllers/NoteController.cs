using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI01.Domain;
using WebAPI01.Infrastructure;
using WebAPI01.Infrastructure.Data;
using WebAPI01.Infrastructure.Repositories;

namespace WebAPI01.API.Controllers
{
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly Context _context;
        private NoteRepository _noteRepository;

        public NoteController(Context context)
        {
            _context = context;
            _noteRepository = new NoteRepository(_context);
        }

        // GET: api/notes
        [HttpGet]
        [Route("api/note")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            //return await _context.Persons.ToListAsync();
            return await _noteRepository.GetAllAsync();
        }

        // POST: api/note
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api/note")]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            try
            {
                note.CreatedAt = DateTime.Now;
                await _noteRepository.AddAsync(note);

                return Created("api/note", note.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
