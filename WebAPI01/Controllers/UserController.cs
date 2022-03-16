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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserRepository _userRepository;
        public UserController(Context context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetPersons()
        {
            //return await _context.Persons.ToListAsync();
            return await _userRepository.GetAllAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetPerson(Guid id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var person = await _userRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, User person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            /*
            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */

            await _userRepository.UpdateAsync(person);

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostPerson(User person)
        {
            //_context.Persons.Add(person);
            //await _context.SaveChangesAsync();
            await _userRepository.AddAsync(person);
            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var person = await _userRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            //_context.Persons.Remove(person);
            //await _context.SaveChangesAsync();
            await _userRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool PersonExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
