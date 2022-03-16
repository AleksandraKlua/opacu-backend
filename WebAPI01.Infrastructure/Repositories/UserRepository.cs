using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI01.Domain;
using Microsoft.EntityFrameworkCore;
using WebAPI01.Domain.Repositories;
using WebAPI01.Infrastructure.Data;

namespace WebAPI01.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public UserRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.OrderBy(p => p.LastName).ToListAsync();
        }
        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task AddAsync(User person)
        {
            _context.Users.Add(person);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(User person)
        {
            var existPerson = await _context.Users.FindAsync(person.Id);
            _context.Entry(existPerson).CurrentValues.SetValues(person);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var person = await _context.Users.FindAsync(id);
            _context.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
