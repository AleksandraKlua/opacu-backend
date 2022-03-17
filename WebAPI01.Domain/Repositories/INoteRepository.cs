using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI01.Domain.Repositories
{
    public interface INoteRepository
    {
        public Task AddAsync(Note note);
        public Task<List<Note>> GetAllAsync();
    }
}
